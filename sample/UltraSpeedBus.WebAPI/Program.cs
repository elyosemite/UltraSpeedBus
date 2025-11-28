using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.Abstractions.Mediator;
using UltraSpeedBus.Extensions.DepedencyInjection;
using UltraSpeedBus.WebAPI.CommandHandler;
using UltraSpeedBus.WebAPI.EventHandler;
using UltraSpeedBus.WebAPI.QueryHandler;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUltraSpeedBus();

builder.Services.AddSingleton<ICommandHandler<CreateOrder, OrderResult>, CreateOrderHandler>();
builder.Services.AddSingleton<IQueryHandler<GetOrder, OrderDto?>, GetOrderQueryHandler>();
builder.Services.AddSingleton<IEventProcessor<OrderCreatedEvent>, OrderCreatedEventHandler>();
builder.Services.AddSingleton<IEventProcessor<OrderAddedToInventoryEvent>, InventoryEventHandler>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

IMediator mediator = app.Services.GetRequiredService<IMediator>();

mediator.RegisterCommandHandler<CreateOrder, OrderResult>(
    (ctx) => app.Services.GetRequiredService<ICommandHandler<CreateOrder, OrderResult>>().Handle(ctx));

mediator.RegisterQueryHandler<GetOrder, OrderDto?>(
    (ctx) => app.Services.GetRequiredService<IQueryHandler<GetOrder, OrderDto?>>().Handle(ctx));

mediator.RegisterEventHandler<OrderCreatedEvent>(
    (ctx) => app.Services.GetRequiredService<IEventProcessor<OrderCreatedEvent>>().Handle(ctx));

app.MapPost("/orders", async (CreateOrder command, ISend sender) =>
{
    OrderResult result = await sender.SendAsync<CreateOrder, OrderResult>(command);
    return Results.Ok(result);
});

// GET /orders/{id} -> Send Query
app.MapGet("/orders/{id:int}", async (int id, ISend sender) =>
{
    OrderDto? result = await sender.SendAsync<GetOrder, OrderDto?>(new GetOrder(id));
    if (result is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(result);
});

// POST /simulate -> Publish Event directly
app.MapPost("/simulate", async (IPublish publisher) =>
{
    await publisher.PublishAsync(new OrderCreatedEvent(999));
    return Results.Ok("Event Published");
});

// Example: Dynamic event consumer (runtime registration)
mediator.ConnectHandlerAsync<OrderCreatedEvent>(async ctx
    => Console.WriteLine($"[Dynamic Consumer] Order created with {ctx.Message.orderId}"));

mediator.ConnectHandlerAsync<OrderAddedToInventoryEvent>(async ctx
    => Console.WriteLine($"[Dynamic Consumer] Order added to inventory with {ctx.Message.orderId}, quantity: {ctx.Message.quantity}, sku: {ctx.Message.sku}"));

await app.RunAsync();
