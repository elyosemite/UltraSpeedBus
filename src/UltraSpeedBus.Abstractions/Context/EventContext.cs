namespace UltraSpeedBus.Abstractions.Contracts;

public class EventContext<TEvent>(TEvent @event)
{
    public TEvent Event { get; } = @event;
}
