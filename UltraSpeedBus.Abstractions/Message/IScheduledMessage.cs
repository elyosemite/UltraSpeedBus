namespace UltraSpeedBus.Abstractions.Message;

public interface IScheduledMessage : IMessage
{
    DateTime ScheduledTime { get; }
}
