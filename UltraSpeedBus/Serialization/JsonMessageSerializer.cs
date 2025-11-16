using System.Text.Json;
using UltraSpeedBus.Abstractions.Message;
using UltraSpeedBus.Abstractions.Serializer;

namespace UltraSpeedBus.Serialization;

public class JsonMessageSerializer : IMessageSerializer
{
    private readonly JsonSerializerOptions _options;
    public string ContentType => throw new NotImplementedException();
    
    public MessageEnvelope Deserialize(ReadOnlyMemory<byte> body, IReadOnlyDictionary<string, object?> headers)
    {
        using var doc = JsonDocument.Parse(body);
        var root = doc.RootElement;

        // Try to reconstruct envelope metadata; payload is left as JsonElement
        var messageId = root.TryGetProperty("messageId", out var mid) && mid.ValueKind == JsonValueKind.String
            ? Guid.Parse(mid.GetString()!)
            : Guid.NewGuid();

        Guid? correlationId = null;
        if (root.TryGetProperty("correlationId", out var cid) && cid.ValueKind == JsonValueKind.String)
            correlationId = Guid.Parse(cid.GetString()!);

        var timestamp = root.TryGetProperty("timestamp", out var ts) && ts.ValueKind == JsonValueKind.String
            ? DateTime.Parse(ts.GetString()!)
            : DateTime.UtcNow;

        object payload = null!;
        if (root.TryGetProperty("payload", out var p))
        {
            // Keep payload as JsonElement for later conversion by consumer
            payload = p.Clone();
        }

        // Merge headers from transport with envelope headers (transport headers may override)
        var envelopeHeaders = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
        foreach (var kv in headers) envelopeHeaders[kv.Key] = kv.Value;

        // if envelope-level headers exist in JSON, merge them too
        if (root.TryGetProperty("headers", out var hdrs) && hdrs.ValueKind == JsonValueKind.Object)
        {
            foreach (var prop in hdrs.EnumerateObject())
            {
                envelopeHeaders[prop.Name] = prop.Value.ValueKind == JsonValueKind.String ? prop.Value.GetString() : prop.Value.ToString();
            }
        }

        var envelope = new MessageEnvelope(payload)
        {
            MessageId = messageId,
            CorrelationId = correlationId,
            Timestamp = timestamp,
            Headers = envelopeHeaders
        };

        return envelope;
    }

    public ReadOnlyMemory<byte> Serialize(Abstractions.Message.MessageEnvelope envelope)
    {
        // Represent envelope as object: metadata + payload
        var wrapper = new
        {
            envelope.MessageId,
            envelope.CorrelationId,
            envelope.Timestamp,
            Headers = envelope.Headers,
            Payload = envelope.Payload
        };

        var bytes = JsonSerializer.SerializeToUtf8Bytes(wrapper, _options);
        return bytes;
    }

    public JsonMessageSerializer(JsonSerializerOptions? options = null)
    {
        _options = options ?? new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };
    }
}
