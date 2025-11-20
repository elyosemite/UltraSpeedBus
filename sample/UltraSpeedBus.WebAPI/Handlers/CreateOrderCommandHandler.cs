using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.WebAPI.Handlers;

public sealed record OrderDto(Guid OrderId, string CustomerName, DateTime CreatedAt);
public sealed record CreateOrderCommand(Guid OrderId, string CustomerName) : ICommand;
public sealed record OrderCreatedEvent(Guid OrderId, DateTime CreatedAt) : IEvent;
public sealed record GetOrderQuery(Guid OrderId) : IQuery<OrderDto>;


public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand>
{
    public async Task HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        // Simulate some processing time
        await Task.Delay(500, cancellationToken);

        Console.WriteLine($"Order created: {command.OrderId} for {command.CustomerName}");
    }
}
