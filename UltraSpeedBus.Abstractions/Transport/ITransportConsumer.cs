namespace UltraSpeedBus.Abstractions.Transport;

public interface ITransportConsumer : IAsyncDisposable
{
    Task StartAsync(Func<ConsumerTransportContext, Task> onMessage, CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);
}
