using UltraSpeedBus.Abstractions;
using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.Abstractions.Mediator;
using UltraSpeedBus.Extensions.DepedencyInjection;
using UltraSpeedBus.WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUltraSpeedBus();

builder.Services.AddSingleton<ICommandHandler<CreateOrder, OrderResult>, CreateOrderHandler>();
builder.Services.AddSingleton<IQueryHandler<GetOrder, OrderDto?>, GetOrderQueryHandler>();
builder.Services.AddSingleton<IEventHandler<OrderCreated>, OrderCreatedEventHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var mediator = app.Services.GetRequiredService<IMediator>();

mediator.RegisterCommandHandler<CreateOrder, OrderResult>(
    (ctx) => app.Services.GetRequiredService<ICommandHandler<CreateOrder, OrderResult>>().Handle(ctx)
);

mediator.RegisterQueryHandler<GetOrder, OrderDto?>(
    (ctx) => app.Services.GetRequiredService<IQueryHandler<GetOrder, OrderDto?>>().Handle(ctx)
);

mediator.RegisterEventHandler<OrderCreated>(
    (ctx) => app.Services.GetRequiredService<IEventHandler<OrderCreated>>().Handle(ctx)
);

app.MapPost("/orders", async (CreateOrder command, ISend sender) =>
{
    var result = await sender.SendAsync<CreateOrder, OrderResult>(command);
    return Results.Ok(result);
});

// GET /orders/{id} -> Send Query
app.MapGet("/orders/{id:int}", async (int id, ISend sender) =>
{
    var result = await sender.SendAsync<GetOrder, OrderDto?>(new GetOrder(id));
    if (result is null)
        return Results.NotFound();

    return Results.Ok(result);
});

// POST /simulate -> Publish Event directly
app.MapPost("/simulate", async (IPublish publisher) =>
{
    await publisher.PublishAsync(new OrderCreated(999));
    return Results.Ok("Event Published");
});

// Example: Dynamic event consumer (runtime registration)
mediator.ConnectHandlerAsync<OrderCreated>(async ctx =>
{
    Console.WriteLine($"[Dynamic Consumer] Order created with {ctx.Message.OrderId}");
});

app.Run();
