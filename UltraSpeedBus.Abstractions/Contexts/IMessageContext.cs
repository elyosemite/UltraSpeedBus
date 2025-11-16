namespace UltraSpeedBus.Abstractions.Contexts;

using System.Threading;

public interface IMessageContext
{
    Guid? MessageId { get; }
    Guid? CorrelationId { get;  }
    DateTime Timestamp { get; }

    IReadOnlyDictionary<string, object?> Headers { get; }
    CancellationToken CancelationToken { get; }
}
