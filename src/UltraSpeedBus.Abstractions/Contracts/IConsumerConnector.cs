namespace UltraSpeedBus.Abstractions.Contracts;

public interface IConsumerConnector
{
    IHandlerHandle ConnectHandlerAsync<TMessage>(Func<ConsumeContext<TMessage>, Task> handler);
}
