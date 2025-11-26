namespace UltraSpeedBus.Abstractions.Contracts;

// TODO implementar IDisposable
public interface IHandlerHandle
{
    void Disconnect();
}

public interface IDynamicHandler : IHandlerHandle
{
    Type MessageType { get; }

    // Handler is typed to generic publishing
    Task Handle(object mesage);
}