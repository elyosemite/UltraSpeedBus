namespace UltraSpeedBus.Abstractions.Message;

/// <summary>
/// Represents a transport-agnostic consumer interface that supports multiple message consumption patterns:
/// - Long polling (e.g., AWS SQS)
/// - Push-based delivery (e.g., Azure Service Bus)
/// - Subscription streaming (e.g., Apache Kafka)
/// - Polling loop (e.g., Redis Streams)
/// </summary>
public interface ITransportConsumer : IAsyncDisposable
{
    Task InitializeAsync(CancellationToken cancellationToken = default);

    Task StartConsumingAsync(
        Func<ConsumerTransportContext, Task> handler,
        CancellationToken cancellationToken = default);

    Task StopConsumingAsync(CancellationToken cancellationToken = default);
}

public class ConsumerTransportContext
{
}