using UltraSpeedBus.Serialization;
using UltraSpeedBus.Transports.InMemory;

namespace UltraSpeedBus.Tests;

public class InMemoryTransportTests
{
    [Test]
    public async Task InMemoryTransport_Send_Publishes_And_Consumer_Completes()
    {
        var serializer = new JsonMessageSerializer();
        var transport = new InMemoryTransport(serializer);

        await transport.StartAsync();
        var received = 0;

        await transport.StartAsync(async ctx =>
        {
            received++;
            await ctx.CompleteAsync();
        });

        var envelope = MessageFactory.Create(new { Text = "hello" }, Guid.NewGuid());
        var sendTask = transport.SendAsync(envelope);

        // Allow consumer loop to pick message
        await Task.Delay(50);

        // sendTask should complete when consumer calls CompleteAsync
        await sendTask;
        Assert.That(received, Is.EqualTo(1));

        await transport.StopAsync();
        await transport.DisposeAsync();
    }
}
