namespace UltraSpeedBus.Abstractions.Contracts;

public interface ISend
{
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request);
}