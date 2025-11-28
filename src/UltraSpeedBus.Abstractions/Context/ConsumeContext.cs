namespace UltraSpeedBus.Abstractions.Contracts;

public class ConsumeContext<T>(T message)
{
    public T Message { get; } = message;
}
