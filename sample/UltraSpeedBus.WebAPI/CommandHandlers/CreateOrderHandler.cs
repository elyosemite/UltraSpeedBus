using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSpeedBus.WebAPI.CommandHandler;

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
