namespace UltraSpeedBus.Abstractions.Message;

public interface ITransport
{
    ITransportProducer CreateProducer();
    ITransportConsumer CreateConsumer(string queueOrTopic);
}