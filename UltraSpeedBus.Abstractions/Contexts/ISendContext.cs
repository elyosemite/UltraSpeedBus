namespace UltraSpeedBus.Abstractions.Contexts;

public interface ISendContext : IMessageContext
{
    object Payload { get; }
    void AddHeader(string key, object? value);
}
