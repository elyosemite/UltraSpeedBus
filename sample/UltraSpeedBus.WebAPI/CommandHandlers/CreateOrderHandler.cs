using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSpeedBus.WebAPI.CommandHandler;

public sealed record CreateOrder(string product, int quantity);
public sealed record OrderResult(int orderId);
public sealed record OrderCreatedEvent(int orderId);
public sealed record OrderAddedToInventoryEvent(int orderId, int quantity, string sku);

public class CreateOrderHandler : ICommandHandler<CreateOrder, OrderResult>
{
    private readonly IPublish _mediator;

    public CreateOrderHandler(IPublish mediator) => _mediator = mediator;

    public Task<OrderResult> Handle(CommandContext<CreateOrder> request)
    {
        // Simula criação de pedido
        int generatedId = Random.Shared.Next(1000, 9999);

        _mediator.PublishAsync(new OrderCreatedEvent(generatedId));
        _mediator.PublishAsync(new OrderAddedToInventoryEvent(generatedId, request.Command.quantity, request.Command.product));

        return Task.FromResult(new OrderResult(generatedId));
    }
}
