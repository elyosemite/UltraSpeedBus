using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.WebAPI.CommandHandler;

namespace UltraSpeedBus.WebAPI.EventHandler;

public class InventoryEventHandler : IEventProcessor<OrderAddedToInventoryEvent>
{
    public Task Handle(EventContext<OrderAddedToInventoryEvent> context)
    {
        Console.WriteLine($"[Event] Order added to inventoty → Id = {context.Event.orderId}, Quantity = {context.Event.quantity}, SKU = {context.Event.sku}");
        return Task.CompletedTask;
    }
}
