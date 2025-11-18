namespace UltraSpeedBus.Abstractions.Message;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task HandleAsync(TQuery query, CancellationToken cancellationToken);
}