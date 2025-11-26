namespace UltraSpeedBus.Abstractions.Contracts;

public interface IConsumerRegister
{
    void RegisterCommandHandler<TCommand, TResponse>(Func<CommandContext<TCommand>, Task<TResponse>> handler);
    void RegisterQueryHandler<TQuery, TResponse>(Func<QueryContext<TQuery>, Task<TResponse>> handler);
    void RegisterEventHandler<TEvent>(Func<EventContext<TEvent>, Task> handler);
}