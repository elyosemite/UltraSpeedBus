namespace UltraSpeedBus.Abstractions.Contracts;

public interface ICommandHandler<TCommand, TResult>
{
    Task<TResult> Handle(CommandContext<TCommand> request);
}
