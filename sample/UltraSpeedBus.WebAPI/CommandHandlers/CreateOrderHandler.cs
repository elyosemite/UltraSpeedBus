using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSpeedBus.WebAPI.CommandHandler;

public sealed record CreateOrder(string product, int quantity);
public sealed record OrderResult(int orderId);
public sealed record OrderCreated(int orderId);

public class CreateOrderHandler : ICommandHandler<CreateOrder, OrderResult>
{
    public Task<OrderResult> Handle(CommandContext<CreateOrder> request)
    {
        // Simula criação de pedido
        int generatedId = Random.Shared.Next(1000, 9999);

        return Task.FromResult(new OrderResult(generatedId));
    }
}
