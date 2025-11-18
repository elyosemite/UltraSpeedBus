namespace UltraSpeedBus.Abstractions.Message;

public class MessageEnvelope
{
    public Guid MessageId { get; init; }
    public string MessageType { get; init; } = null!;
    public byte[] Payload { get; init; } = null!;
}

/// <summary>
/// Defines a transport producer capable of sending and publishing messages.
/// it will be used for Azure Service Bus, AWS, RabbitMQ fanout, SQS, Redis Streams, etc.
/// </summary>
public interface ITransportProducer : IAsyncDisposable
{
    Task InitializeAsync(CancellationToken cancellationToken = default);

    Task SendAsync(string queue, MessageEnvelope envelop, CancellationToken cancellationToken = default);
    Task PublishAsync(string topic, MessageEnvelope envelop, CancellationToken cancellationToken = default);
}