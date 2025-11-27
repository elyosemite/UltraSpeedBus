using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.WebAPI.CommandHandler;

namespace UltraSpeedBus.WebAPI.EventHandler;

public class OrderCreatedEventHandler : IEventHandler<OrderCreated>
{
    public Task Handle(EventContext<OrderCreated> context)
    {
        Console.WriteLine($"[Event] Order created → Id = {context.Event.OrderId}");
        return Task.CompletedTask;
    }
}
