using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.Abstractions.Mediator;

namespace UltraSppedBus.Abstractions.Mediator;

/// <summary>
/// The main mediator implementation.
/// </summary>
public partial class UltraMediator : IMediator
{
    private readonly Dictionary<Type, Func<object, Task<object>>> _commandHandlers = new ();
    private readonly Dictionary<Type, Func<object, Task<object>>> _queryHandlers = new ();
    private readonly Dictionary<Type, List<Func<object, Task>>> _eventHandlers = new ();
    private readonly Dictionary<Type, List<IDynamicHandler>> _dynamicHandlers = new ();

    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
    {
        Type type = typeof(TRequest);

        if (_commandHandlers.TryGetValue(type, out Func<object, Task<object>>? handler))
        {
            return InvokeHandler<TRequest, TResponse>(handler, request);
        }

        if (_queryHandlers.TryGetValue(type, out Func<object, Task<object>>? queryHandler))
        {
            return InvokeHandler<TRequest, TResponse>(queryHandler, request);
        }

        throw new InvalidOperationException($"No handler registered for {type.Name}");
    }

    public Task PublishAsync<TEvent>(TEvent message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Type type = typeof(TEvent);
        var tasks = new List<Task>();

        if (_eventHandlers.TryGetValue(type, out List<Func<object, Task>>? eventHandlers))
        {
            foreach (Func<object, Task> handler in eventHandlers)
            {
                tasks.Add(handler(message));
            }
        }

        // You can disable this one
        if (_dynamicHandlers.TryGetValue(type, out List<IDynamicHandler>? dynamicEventHandlers))
        {
            foreach (DynamicHandler<TEvent> handler in dynamicEventHandlers.OfType<DynamicHandler<TEvent>>())
            {
                tasks.Add(handler.Handle(message));
            }
        }

        return Task.WhenAll(tasks);
    }

    public IHandlerHandle ConnectHandlerAsync<TMessage>(Func<ConsumeContext<TMessage>, Task> handler)
    {
        var dynamicHandler = new DynamicHandler<TMessage>(this, handler);

        lock (_dynamicHandlers)
        {
            if (!_dynamicHandlers.TryGetValue(typeof(TMessage), out List<IDynamicHandler>? list))
            {
                list = new List<IDynamicHandler>();
                _dynamicHandlers.Add(typeof(TMessage), list);
            }

            list.Add(dynamicHandler);
        }

        return dynamicHandler;
    }

    public void RegisterCommandHandler<TCommand, TResponse>(Func<CommandContext<TCommand>, Task<TResponse>> handler)
        => _commandHandlers[typeof(TCommand)] = async (object cmd) =>
        {
            var typed = (TCommand)cmd;
            var ctx = new CommandContext<TCommand>(typed);
            TResponse? resp = await handler(ctx);
            return resp!;
        };

    public void RegisterQueryHandler<TQuery, TResponse>(Func<QueryContext<TQuery>, Task<TResponse>> handler)
        => _queryHandlers[typeof(TQuery)] = async q =>
            {
                var typed = (TQuery)q;
                var ctx = new QueryContext<TQuery>(typed);
                TResponse? resp = await handler(ctx);
                return resp!;
            };

    public void RegisterEventHandler<TEvent>(Func<EventContext<TEvent>, Task> handler)
    {
        if (!_eventHandlers.TryGetValue(typeof(TEvent), out List<Func<object, Task>>? list))
        {
            list = new List<Func<object, Task>>();
            _eventHandlers.Add(typeof(TEvent), list);
        }

        list.Add(EventConsumer(handler));
    }

    internal void RemoveDynamicHandler(IDynamicHandler handler)
    {
        lock (_dynamicHandlers)
        {
            if (_dynamicHandlers.TryGetValue(handler.MessageType, out List<IDynamicHandler>? list))
            {
                list.Remove(handler);

                if (list.Count == 0)
                {
                    _dynamicHandlers.Remove(handler.MessageType);
                }
            }
        }
    }

    private static Func<object, Task> EventConsumer<TEvent>(
        Func<EventContext<TEvent>, Task> handler) =>
            evt =>
                {
                    var typed = (TEvent)evt;
                    var ctx = new EventContext<TEvent>(typed);
                    return handler(ctx);
                };

    private static async Task<TResponse> InvokeHandler<TRequest, TResponse>(
        Func<object, Task<object>> handler,
        TRequest request)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        object response = await handler(request);
        return (TResponse)response;
    }
}
