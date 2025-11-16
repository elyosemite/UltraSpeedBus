using UltraSpeedBus.Abstractions.Contexts;
using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Contexts;

public class ConsumerContext : MessageContext, IConsumerContext
{
    public object Message => _envelope.Payload;
    public int DeliveryCount { get; }

    public ConsumerContext(
        MessageEnvelope envelope,
        int deliveryCount,
        CancellationToken cancellationToken)
        : base(envelope, cancellationToken)
    {
        DeliveryCount = deliveryCount;
    }
}
