namespace UltraSpeedBus.Abstractions.Contracts;

public class CommandContext<TCommand>
{
    public TCommand Command { get; }
    public CommandContext(TCommand command) => Command = command;
}
