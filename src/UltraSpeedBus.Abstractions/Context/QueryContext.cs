namespace UltraSpeedBus.Abstractions.Contracts;

public class QueryContext<TQuery>
{
    public TQuery Query { get; }
    public QueryContext(TQuery query) => Query = query;
}
