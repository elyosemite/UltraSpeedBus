namespace UltraSpeedBus.Abstractions.Contracts;

public class CommandContext<TCommand>(TCommand command)
{
    public TCommand Command { get; } = command;
}
