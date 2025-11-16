using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Abstractions.Transport;

public class ConsumerTransportContext
{
    public MessageEnvelope Envelope { get; }
    public int DeliveryCount { get; }
    public Func<Task> CompleteAsync { get; }
    public Func<Task> AbandonAsync { get; }
    public Func<string, string, Task> DeadLetterAsync { get; } // reason, description
    public CancellationToken CancellationToken { get; }

    public ConsumerTransportContext(
        MessageEnvelope envelope,
        int deliveryCount,
        Func<Task> completeAsync,
        Func<Task> abandonAsync,
        Func<string, string, Task> deadLetterAsync,
        CancellationToken cancellationToken)
    {
        Envelope = envelope;
        DeliveryCount = deliveryCount;
        CompleteAsync = completeAsync;
        AbandonAsync = abandonAsync;
        DeadLetterAsync = deadLetterAsync;
        CancellationToken = cancellationToken;
    }
}
