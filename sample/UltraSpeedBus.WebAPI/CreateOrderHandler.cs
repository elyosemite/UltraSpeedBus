using UltraSpeedBus.Abstractions;
using UltraSpeedBus.Abstractions.Contracts;


namespace UltraSpeedBus.WebAPI;


#region Command
public sealed record CreateOrder(string Product, int Quantity);
public sealed record OrderResult(int OrderId);
public sealed record OrderCreated(int OrderId);

public class CreateOrderHandler : ICommandHandler<CreateOrder, OrderResult>
{
    public Task<OrderResult> Handle(CommandContext<CreateOrder> request)
    {
        // Simula criação de pedido
        int generatedId = Random.Shared.Next(1000, 9999);

        return Task.FromResult(new OrderResult(generatedId));
    }
}
#endregion


#region Query
public sealed record GetOrder(int OrderId);
public sealed record OrderDto(int OrderId, string Description);
public class GetOrderQueryHandler : IQueryHandler<GetOrder, OrderDto?>
{
    public Task<OrderDto?> Handle(QueryContext<GetOrder> context)
    {
        if (context.Query.OrderId == 42)
        {
            return Task.FromResult<OrderDto?>(new OrderDto(42, "Example Order"));
        }

        return Task.FromResult<OrderDto?>(null);
    }
}
#endregion

#region Event
public class OrderCreatedEventHandler : IEventHandler<OrderCreated>
{
    public Task Handle(EventContext<OrderCreated> context)
    {
        Console.WriteLine($"[Event] Order created → Id = {context.Event.OrderId}");
        return Task.CompletedTask;
    }
}
#endregion