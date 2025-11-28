namespace UltraSpeedBus.Abstractions.Contracts;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> Handle(QueryContext<TQuery> request);
}
