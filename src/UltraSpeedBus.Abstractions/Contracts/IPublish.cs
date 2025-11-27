namespace UltraSpeedBus.Abstractions.Contracts;

public interface IPublish
{
    Task PublishAsync<TEvent>(TEvent message);
}
