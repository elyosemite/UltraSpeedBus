using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Tests;

public class MessageEnvelopeTests
{
    private class TestMessage { public required string Text { get; set; } }

    [Test]
    public void Envelope_Should_Create_Default_Fields()
    {
        var msg = new TestMessage
        {
            Text = "Text"
        };

        var envelope = new MessageEnvelope(msg);

        Assert.That(envelope.Payload, Is.EqualTo(msg));
        Assert.That(envelope.MessageId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(envelope.Timestamp, Is.LessThanOrEqualTo(DateTime.UtcNow));
        Assert.That(envelope.Headers, Is.Not.Null);
    }
}