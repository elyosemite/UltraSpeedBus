using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Abstractions.Transport;

public interface ITransportProducer : IAsyncDisposable
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task StopAsync(CancellationToken cancellationToken = default);

    Task SendAsync(MessageEnvelope envolope, CancellationToken cancellationToken = default);
    Task PublishAsync(MessageEnvelope envolope, CancellationToken cancellationToken = default);
}
