namespace UltraSpeedBus.Abstractions.Contracts;

public interface IHandlerHandle
{
    void Disconnect();
}

public interface IDynamicHandler : IHandlerHandle
{
    Type MessageType { get; }

    // Handler is typed to generic publishing
    Task Handle(object message);
}
