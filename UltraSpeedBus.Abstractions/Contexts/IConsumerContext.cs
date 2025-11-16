namespace UltraSpeedBus.Abstractions.Contexts;

public interface IConsumerContext : IMessageContext
{
    object Message { get; }
    int DeliveryCount { get; }
}
