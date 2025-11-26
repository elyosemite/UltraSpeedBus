using UltraSpeedBus.Abstractions.Contracts;

namespace UltraSpeedBus.Abstractions;

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> Handle(CommandContext<TCommand> request);
}

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> Handle(QueryContext<TQuery> request);
}

public interface IEventHandler<TEvent>
{
    Task Handle(EventContext<TEvent> request);
}