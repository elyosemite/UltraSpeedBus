using UltraSpeedBus.Contexts;

namespace UltraSpeedBus.Tests.Context;

internal class MessageContext
{
    [Test]
    public void MessageContext_Should_Expose_Envelope_Properties()
    {
        var envelope = MessageFactory.Create("hello", correlationId: Guid.NewGuid());
        var ctx = new SendContext(envelope, CancellationToken.None);

        Assert.That(ctx.MessageId, Is.EqualTo(envelope.MessageId));
        Assert.That(ctx.CorrelationId, Is.EqualTo(envelope.CorrelationId));
        Assert.That(ctx.Timestamp, Is.EqualTo(envelope.Timestamp));
    }
}
