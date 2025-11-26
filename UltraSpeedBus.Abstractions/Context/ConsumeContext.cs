namespace UltraSpeedBus.Abstractions.Contracts;

public class ConsumeContext<T>
{
    public T Message { get; }
    public ConsumeContext(T message) => Message = message;
}
