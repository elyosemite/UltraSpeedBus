# UltraSpeedBus
UltraSpeedBus is a free, open-source messaging framework for .NET, engineered for **high-performance** and reliability in distributed systems. It simplifies building scalable applications through **message-based, asynchronous communication**, enabling loose coupling between services while delivering enhanced availability, fault tolerance, and scalability.

## Packages

| Package Name                 | Description                                                                                   |
| ---------------------------- | --------------------------------------------------------------------------------------------- |
| `UltraSpeedBus`              | The core library with message transport, context, pipelines, and integration implementations. |
| `UltraSpeedBus.Abstractions` | Contains the core contracts, interfaces, and message envelope definitions for the system.     |

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
```

```csharp
using UltraSpeedBus;
using UltraSpeedBus.Abstractions;

// Create a message
var message = new MyCommand { Name = "Test" };
var envelope = MessageFactory.Create(message);

// Send using your transport implementation (e.g., Azure Service Bus)
await producer.SendAsync(envelope);
```

## Contributing

Contributions are welcome! Please open issues or submit pull requests to help improve UltraSpeedBus.

## License

This project is licensed under the **MIT License**.
