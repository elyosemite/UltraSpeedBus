using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Message;

/// <summary>
/// Mediator-style dispatcher that routes Commands, Events and Queries through DI-resolved handlers
/// and a middleware pipeline.
/// </summary>
public class UltraSpeedBusMediator : IUltraSpeedBusMediator
{
    private readonly HandlerRegistry _registry;
    private readonly IServiceProvider _services;
    private readonly DefaultPipeline _pipeline;

    public UltraSpeedBusMediator(HandlerRegistry registry, IServiceProvider services, DefaultPipeline pipeline)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        _services = services ?? throw new ArgumentNullException(nameof(services));
        _pipeline = pipeline ?? throw new ArgumentNullException(nameof(pipeline));
    }

    #region Send
    public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : ICommand
    {
        if (command == null) throw new ArgumentNullException(nameof(command));

        var handlerObj = _registry.GetCommandHandler(typeof(TCommand));
        if (handlerObj == null)
            throw new InvalidOperationException($"No handler registered for command type ICommandHandler<{typeof(TCommand).FullName}>");
        
        var context = new MessageInvocationContext(command, typeof(TCommand), _services, cancellationToken);

        async Task FinalHandler()
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(typeof(TCommand));
            using var scope = _services.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            var method = handlerType.GetMethod("HandleAsync", BindingFlags.Instance | BindingFlags.Public);
            if (method == null) throw new InvalidOperationException("Handler has no HandleAsync method.");
            var task = (Task)method.Invoke(handler, new object[] { command, cancellationToken })!;
            await task.ConfigureAwait(false);
        }

        await _pipeline.ExecuteAsync(context, FinalHandler).ConfigureAwait(false);
    }
    #endregion

    #region Publish (Event)

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : IEvent
    {
        if (@event == null) throw new ArgumentNullException(nameof(@event));

        var handlers = _registry.GetEventHandlers(typeof(TEvent)).ToArray();

        var context = new MessageInvocationContext(@event, typeof(TEvent), _services, cancellationToken);

        if (handlers.Length == 0)
        {
            // Still run pipeline with no-op final.
            await _pipeline.ExecuteAsync(context, () => Task.CompletedTask).ConfigureAwait(false);
            return;
        }

        // For events, invoke each handler inside pipeline. We will execute handlers concurrently,
        // but each handler invocation goes through pipeline (so middlewares run per handler).
        var tasks = handlers.Select(handlerObj => InvokeEventHandler(handlerObj, @event, cancellationToken));
        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    private async Task InvokeEventHandler(object handlerObj, object @event, CancellationToken cancellationToken)
    {
        var handlerType = handlerObj.GetType().GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>));
        if (handlerType == null) return;

        var messageType = handlerType.GetGenericArguments()[0];
        var context = new MessageInvocationContext(@event, messageType, _services, cancellationToken);

        async Task FinalHandler()
        {
            using var scope = _services.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);
            var method = handlerType.GetMethod("HandleAsync", BindingFlags.Instance | BindingFlags.Public);
            if (method == null) throw new InvalidOperationException("Event handler has no HandleAsync method.");
            var task = (Task)method.Invoke(handler, new object[] { @event, cancellationToken })!;
            await task.ConfigureAwait(false);
        }

        await _pipeline.ExecuteAsync(context, FinalHandler).ConfigureAwait(false);
    }
    #endregion

    #region Query
    public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
        where TQuery : IQuery<TResult>
    {
        if (query == null) throw new ArgumentNullException(nameof(query));

        var queryHandlerInterface = typeof(IQueryHandler<,>).MakeGenericType(typeof(TQuery), typeof(TResult));
        var handlerObj = _registry.GetQueryHandler(queryHandlerInterface);
        if (handlerObj == null)
            throw new InvalidOperationException($"No IQueryHandler<{typeof(TQuery).Name},{typeof(TResult).Name}> registered.");

        var context = new MessageInvocationContext(query, typeof(TQuery), _services, cancellationToken);

        TResult result = default!;

        async Task FinalHandler()
        {
            using var scope = _services.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService(queryHandlerInterface);
            var method = queryHandlerInterface.GetMethod("HandleAsync", BindingFlags.Instance | BindingFlags.Public);
            if (method == null) throw new InvalidOperationException("Query handler has no HandleAsync method.");
            var task = (Task)method.Invoke(handler, new object[] { query, cancellationToken })!;
            await task.ConfigureAwait(false);
            // If method is Task<TResult>, we need to get Result property via reflection
            var resultProperty = task.GetType().GetProperty("Result");
            if (resultProperty != null)
            {
                result = (TResult)resultProperty.GetValue(task)!;
            }
            else
            {
                // For Task with no Result (should not happen)
                result = default!;
            }
        }

        await _pipeline.ExecuteAsync(context, FinalHandler).ConfigureAwait(false);
        return result;
    }
    #endregion Query
}