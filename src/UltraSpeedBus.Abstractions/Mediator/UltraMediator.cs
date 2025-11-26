using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.Abstractions.Mediator;

namespace UltraSppedBus.Abstractions.Mediator;

public class UltraMediator : IMediator
{
    private readonly Dictionary<Type, Func<object, Task<object>>> _commandHandlers = new();
    private readonly Dictionary<Type, Func<object, Task<object>>> _queryHandlers = new();
    private readonly Dictionary<Type, List<Func<object, Task>>> _eventHandlers = new();
    private readonly Dictionary<Type, List<IDynamicHandler>> _dynamicHandlers = new();

    #region Implement ISend
    // Publisher 1 command x 1 Consumer
    // Publisher 1 query   x 1 Consumer
    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request)
    {
        var type = typeof(TRequest);

        if (_commandHandlers.TryGetValue(type, out var handler))
        {
            return InvokeHandler<TRequest, TResponse>(handler, request);
        }

        if (_queryHandlers.TryGetValue(type, out var queryHandler))
        {
            return InvokeHandler<TRequest, TResponse>(queryHandler, request);
        }

        throw new InvalidOperationException($"No handler registered for {type.Name}");
    }
    #endregion

    #region Implement IPublish
    // Publisher 1 x Many Consumers
    public Task PublishAsync<TEvent>(TEvent @event)
    {
        var type = typeof(TEvent);
        var tasks = new List<Task>();

        if (_eventHandlers.TryGetValue(type, out var eventHandlers))
        {
            foreach (var handler in eventHandlers)
                tasks.Add(handler(@event));
        }

        // You can disable this one
        if (_dynamicHandlers.TryGetValue(type, out var dynamicEventHandlers))
        {
            foreach (var handler in dynamicEventHandlers.OfType<DynamicHandler<TEvent>>())
                tasks.Add(handler.Handle(@event));
        }

        return Task.WhenAll(tasks);
    }
    #endregion

    #region Implement IConsumerConnector
    public IHandlerHandle ConnectHandlerAsync<TMessage>(Func<ConsumeContext<TMessage>, Task> handler)
    {
        var dynamicHandler = new DynamicHandler<TMessage>(this, handler);

        lock (_dynamicHandlers)
        {
            if (!_dynamicHandlers.TryGetValue(typeof(TMessage), out var list))
            {
                list = new List<IDynamicHandler>();
                _dynamicHandlers.Add(typeof(TMessage), list);
            }
            list.Add(dynamicHandler);
        }
        return dynamicHandler;
    }
    #endregion

    #region Implement IConsumerRegister
    public void RegisterCommandHandler<TCommand, TResponse>(Func<CommandContext<TCommand>, Task<TResponse>> handler)
    {
        _commandHandlers[typeof(TCommand)] = async (object cmd) =>
        {
            var typed = (TCommand)cmd;
            var ctx = new CommandContext<TCommand>(typed);
            var resp = await handler(ctx);
            return resp!;
        };
    }

    public void RegisterQueryHandler<TQuery, TResponse>(Func<QueryContext<TQuery>, Task<TResponse>> handler)
    {
        _queryHandlers[typeof(TQuery)] = async q =>
        {
            var typed = (TQuery)q;
            var ctx = new QueryContext<TQuery>(typed);
            var resp = await handler(ctx);
            return resp!;
        };
    }

    public void RegisterEventHandler<TEvent>(Func<EventContext<TEvent>, Task> handler)
    {
        if (!_eventHandlers.TryGetValue(typeof(TEvent), out var list))
            _eventHandlers[typeof(TEvent)] = list = new List<Func<object, Task>>();
    }
    #endregion

    private async Task<TResponse> InvokeHandler<TRequest, TResponse>(
        Func<object, Task<object>> handler,
        TRequest request
    )
    {
        var response = await handler(request);
        return (TResponse)response;
    }

    internal void RemoveDynamicHandler(IDynamicHandler handler)
    {
        lock (_dynamicHandlers)
        {
            if (_dynamicHandlers.TryGetValue(handler.MessageType, out var list))
            {
                list.Remove(handler);

                if (list.Count == 0)
                    _dynamicHandlers.Remove(handler.MessageType);
            }
        }
    }

    private class DynamicHandler<T> : IDynamicHandler
    {
        private readonly UltraMediator _mediator;
        private readonly Func<ConsumeContext<T>, Task> _handler;

        public DynamicHandler(
            UltraMediator mediator,
            Func<ConsumeContext<T>, Task> handler)
        {
            _mediator = mediator;
            _handler = handler;
        }

        public Type MessageType => typeof(T);
        // Pensa em uma interface para este método futuramente. Melhore o design
        public Task Handle(object msg)
        {
            var ctx = new ConsumeContext<T>((T)msg);
            return _handler(ctx);
        }

        public void Disconnect()
        {
            _mediator.RemoveDynamicHandler(this);
        }
    }
}