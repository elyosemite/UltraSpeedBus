namespace UltraSpeedBus.Abstractions.Message;

public interface ICorrelatedMessage : IMessage
{
    Guid CorrelationId { get; }
}
