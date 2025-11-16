using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus;

public static class MessageFactory
{
    public static MessageEnvelope Create(object message, Guid? correlationId = null)
    {
        var envelope = new MessageEnvelope(message);

        if (correlationId.HasValue)
            envelope.CorrelationId = correlationId.Value;

        return envelope;
    }
}
