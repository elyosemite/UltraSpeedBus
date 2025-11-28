using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.WebAPI.CommandHandler;

namespace UltraSpeedBus.WebAPI.EventHandler;

public class OrderCreatedEventHandler : IEventProcessor<OrderCreatedEvent>
{
    public Task Handle(EventContext<OrderCreatedEvent> context)
    {
        Console.WriteLine($"[Event] Order created → Id = {context.Event.orderId}");
        return Task.CompletedTask;
    }
}
