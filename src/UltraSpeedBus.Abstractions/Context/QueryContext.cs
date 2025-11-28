namespace UltraSpeedBus.Abstractions.Contracts;

public class QueryContext<TQuery>(TQuery query)
{
    public TQuery Query { get; } = query;
}
