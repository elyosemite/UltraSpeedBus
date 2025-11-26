namespace UltraSpeedBus.Abstractions.Contracts;

public class EventContext<TEvent>
{
    public TEvent Event { get; }
    public EventContext(TEvent @event) => Event = @event;
}
