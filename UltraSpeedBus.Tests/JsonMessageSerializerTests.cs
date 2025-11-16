using UltraSpeedBus.Serialization;

namespace UltraSpeedBus.Tests;

public class JsonMessageSerializerTests
{
    [Test]
    public void Serialize_And_Deserialize_Preserves_Metadata_And_Payload()
    {
        var serializer = new JsonMessageSerializer();
        var payload = new { Name = "Alice", Age = 30 };
        var envelope = MessageFactory.Create(payload, correlationId: Guid.NewGuid());
        envelope.Headers["foo"] = "bar";

        var bytes = serializer.Serialize(envelope);
        var headers = new Dictionary<string, object?> { { "transport", "inmem" } };

        var deserialized = serializer.Deserialize(bytes, headers);

        Assert.AreEqual(envelope.MessageId, deserialized.MessageId);
        Assert.AreEqual(envelope.CorrelationId, deserialized.CorrelationId);
        Assert.That(deserialized.Headers.ContainsKey("foo"));
        Assert.That(deserialized.Headers["transport"], Is.EqualTo("inmem"));
        // payload is JsonElement; try to convert
        var je = (System.Text.Json.JsonElement)deserialized.Payload;
        var name = je.GetProperty("name").GetString();
        Assert.AreEqual("Alice", name);
    }
}