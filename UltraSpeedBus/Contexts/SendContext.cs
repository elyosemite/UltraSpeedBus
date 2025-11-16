using UltraSpeedBus.Abstractions.Contexts;
using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Contexts;

public class SendContext : MessageContext, ISendContext
{
    public object Payload => _envelope.Payload;
    public SendContext(
        MessageEnvelope envelope,
        CancellationToken cancellationToken)
        : base(envelope, cancellationToken)
    {
    }
    public void AddHeader(string key, object? value)
    {
        _envelope.Headers[key] = value;
    }
}