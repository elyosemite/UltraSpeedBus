# UltraSpeedBus
UltraSpeedBus is a free, open-source messaging framework for .NET, engineered for **high-performance** and reliability in distributed systems. It simplifies building scalable applications through **message-based, asynchronous communication**, enabling loose coupling between services while delivering enhanced availability, fault tolerance, and scalability.

## Packages

| Package Name                 | Description                                                                                   |
| ---------------------------- | --------------------------------------------------------------------------------------------- |
| `UltraSpeedBus`              | The core library with message transport, context, pipelines, and integration implementations. |
| `UltraSpeedBus.Abstractions` | Contains the core contracts, interfaces, and message envelope definitions for the system.     |
| `UltraSpeedBus.Extensions.DependencyInjection` | Inject your dependencies |

## Features

* **High-performance messaging** for .NET applications
* **Supports distributed architectures** with command, event, and saga patterns
* **Pluggable transports**, starting with Azure Service Bus (others planned: SQL Server, MySQL, PostgreSQL, MongoDB, AWS SQS/SNS, OCI, GCP Pub/Sub)
* **Saga implementations, error handling, and retries**
* **Observability** using OpenTelemetry
* **Free and open-source**, easy to extend for custom requirements

## Getting Started

```bash
# Install the packages via NuGet
dotnet add package UltraSpeedBus
dotnet add package UltraSpeedBus.Abstractions
dotnet add package UltraSpeedBus.Extensions.DependencyInjection
```

# Configure your ``Program``
```cs
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
```

## Command handler

```csharp
using UltraSpeedBus;
using UltraSpeedBus.Abstractions;

// Create a command and command Handler with ICommandHandler
public sealed record CreateOrderCommand(string Product, int Quantity);
public sealed record OrderResult(int OrderId);

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, OrderResult>
{
    public Task<OrderResult> Handle(CommandContext<CreateOrderCommand> request)
    {
        int generatedId = Random.Shared.Next(1000, 9999);
        return Task.FromResult(new OrderResult(generatedId));
    }
}
```

## Query Handler

```cs
public sealed record GetOrderQuery(int OrderId);
public sealed record OrderDto(int OrderId, string Description);

public class GetOrderQueryHandler : IQueryHandler<GetOrderQuery, OrderDto?>
{
    public Task<OrderDto?> Handle(QueryContext<GetOrderQuery> context)
    {
        if (context.Query.OrderId == 42)
        {
            return Task.FromResult<OrderDto?>(new OrderDto(42, "Example Order"));
        }

        return Task.FromResult<OrderDto?>(null);
    }
}
```

## Event Handler

```cs
public sealed record OrderCreatedEvent(int OrderId);

public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
{
    public Task Handle(EventContext<OrderCreatedEvent> context)
    {
        Console.WriteLine($"[Event] Order created → Id = {context.Event.OrderId}");
        return Task.CompletedTask;
    }
}
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests to help improve UltraSpeedBus.

## License

This project is licensed under the **MIT License**.
