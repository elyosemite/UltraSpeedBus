using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSpeedBus.Abstractions.Mediator;

public interface IMediator :
    ISend,
    IPublish,
    IConsumerConnector,
    IConsumerRegister
{
}
