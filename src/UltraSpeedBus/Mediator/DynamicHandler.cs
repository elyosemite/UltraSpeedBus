using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSppedBus.Abstractions.Mediator;

/// <summary>
/// Dynamic handler for events.
/// </summary>
public partial class UltraMediator
{
    private sealed class DynamicHandler<T> : IDynamicHandler
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

        public Task Handle(object message)
        {
            var ctx = new ConsumeContext<T>((T)message);
            return _handler(ctx);
        }

        public void Disconnect() => _mediator.RemoveDynamicHandler(this);
    }
}
