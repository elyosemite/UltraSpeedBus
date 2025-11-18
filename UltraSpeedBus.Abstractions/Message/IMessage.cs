namespace UltraSpeedBus.Abstractions.Message;

public interface IMessage
{
    /// <summary>
    /// It will be used for Saga, Idempotency, etc.
    /// </summary>
    Guid MessageId { get; }

    /// <summary>
    /// It will be used for logging, tracing, etc.
    /// </summary>
    DateTime Timestamp { get; }
}