namespace UltraSpeedBus.Message;

public sealed class MessageInvocationContext
{
    public object Message { get; }
    public Type MessageType { get; }
    public CancellationToken CancellationToken { get; }
    public IServiceProvider Services { get; }

    public MessageInvocationContext(object message, Type messageType, IServiceProvider services, CancellationToken cancellationToken)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        MessageType = messageType ?? throw new ArgumentNullException(nameof(messageType));
        Services = services ?? throw new ArgumentNullException(nameof(services));
        CancellationToken = cancellationToken;
    }
}
