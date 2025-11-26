namespace UltraSpeedBus.Abstractions.Contracts;

public interface IEventHandler<TEvent>
{
    Task Handle(EventContext<TEvent> request);
}