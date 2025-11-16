using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Abstractions.Serializer;

public interface IMessageSerializer
{
    ReadOnlyMemory<byte> Serialize(MessageEnvelope envelope);
    MessageEnvelope Deserialize(ReadOnlyMemory<byte> body, IReadOnlyDictionary<string, object?> headers);
    string ContentType { get; }
}
