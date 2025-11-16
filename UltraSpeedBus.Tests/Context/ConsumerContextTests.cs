using UltraSpeedBus.Contexts;

namespace UltraSpeedBus.Tests.Context;

public class ConsumerContextTests
{
    private class TestMessage { public required string Text { get; set; } }

    [Test]
    public void ConsumerContext_Should_Create_With_Valid_Values()
    {
        var msg = new TestMessage { Text = "Test" };
        var envelope = MessageFactory.Create(msg);
        var context = new ConsumerContext(envelope, deliveryCount: 2, CancellationToken.None);

        Assert.That(context.Message, Is.EqualTo(msg));
        Assert.That(context.DeliveryCount, Is.EqualTo(2));
        Assert.That(context.MessageId, Is.Not.EqualTo(Guid.Empty));
        Assert.That(context.Timestamp, Is.LessThanOrEqualTo(DateTime.UtcNow));
    }
}
