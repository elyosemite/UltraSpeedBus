using UltraSpeedBus.Contexts;

namespace UltraSpeedBus.Tests.Context;

internal class SendContextTests
{
    private class DummyMessage { }

    [Test]
    public void SendContext_Should_Add_Headers()
    {
        var envelope = MessageFactory.Create(new DummyMessage());
        var context = new SendContext(envelope, CancellationToken.None);

        context.AddHeader("key1", "value1");

        Assert.That(context.Headers.ContainsKey("key1"), Is.True);
        Assert.That(context.Headers["key1"], Is.EqualTo("value1"));
    }
}
