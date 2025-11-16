namespace UltraSpeedBus.Tests;

public class MessageFactoryTests
{
    private class DummyMessage { }

    [Test]
    public void MessageFactory_Should_Set_CorrelationId()
    {
        var correlationId = Guid.NewGuid();
        var msg = new DummyMessage();

        var envelope = MessageFactory.Create(msg, correlationId);

        Assert.That(envelope.CorrelationId, Is.EqualTo(correlationId));
    }

    [Test]
    public void MessageFactory_Should_Not_Set_CorrelationId_When_Null()
    {
        var msg = new DummyMessage();
        var envelope = MessageFactory.Create(msg);

        Assert.That(envelope.CorrelationId, Is.Null);
    }
}