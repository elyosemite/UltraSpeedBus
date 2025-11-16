namespace UltraSpeedBus.Abstractions.Transport;

public interface ITransportTopology
{
    Task EnsureQueueExistsAsync(string queueName, CancellationToken cancellationToken = default);
    Task EnsureTopicExistsAsync(string topicName, CancellationToken cancellationToken = default);
    Task EnsureSubscriptionExistsAsync(string topicName, string subscriptionName, CancellationToken cancellationToken = default);
}
