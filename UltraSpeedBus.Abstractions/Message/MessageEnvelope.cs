namespace UltraSpeedBus.Abstractions.Message;

public class MessageEnvelope
{
    public Guid MessageId { get; init; }
    public Guid? CorrelationId { get; set; }
    public DateTime Timestamp { get; init; }
    public Dictionary<string, object?> Headers { get; init; }
    public object Payload { get; init; }

    public MessageEnvelope(object payload)
    {
        Payload = payload ?? throw new ArgumentNullException(nameof(payload));
        Timestamp = DateTime.UtcNow;
        MessageId = Guid.NewGuid();
        Headers = new Dictionary<string, object?>();
    }
}
