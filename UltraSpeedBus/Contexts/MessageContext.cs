using UltraSpeedBus.Abstractions.Contexts;
using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Contexts;

public abstract class MessageContext : IMessageContext
{
    protected readonly MessageEnvelope _envelope;


    public Guid? MessageId => _envelope.MessageId;
    public Guid? CorrelationId => _envelope.CorrelationId;
    public DateTime Timestamp => _envelope.Timestamp;
    public IReadOnlyDictionary<string, object?> Headers => _envelope.Headers;

    public CancellationToken CancelationToken { get; }

    protected MessageContext(MessageEnvelope envelope, CancellationToken cancellationToken)
    {
        _envelope = envelope ?? throw new ArgumentNullException(nameof(envelope));
        CancelationToken = cancellationToken;
    }
}
