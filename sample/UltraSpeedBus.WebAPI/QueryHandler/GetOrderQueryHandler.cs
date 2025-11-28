using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSpeedBus.WebAPI.QueryHandler;

public sealed record GetOrder(int orderId);
public sealed record OrderDto(int orderId, string description);

public class GetOrderQueryHandler : IQueryHandler<GetOrder, OrderDto?>
{
    public Task<OrderDto?> Handle(QueryContext<GetOrder> context)
    {
        if (context.Query.orderId == 42)
        {
            return Task.FromResult<OrderDto?>(new OrderDto(42, "Example Order"));
        }

        return Task.FromResult<OrderDto?>(null);
    }
}
