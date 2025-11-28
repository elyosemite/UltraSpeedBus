namespace UltraSpeedBus.Abstractions.Contracts;

public interface IEventProcessor<TEvent>
{
    Task Handle(EventContext<TEvent> request);
}
