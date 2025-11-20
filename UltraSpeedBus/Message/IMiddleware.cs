namespace UltraSpeedBus.Message;

// <summary>
/// Non-generic middleware for the message pipeline. The middleware receives a context and a `next`.
/// Keep it simple: middleware can inspect context.Message and act accordingly.
/// </summary>
public interface IMiddleware
{
    Task InvokeAsync(MessageInvocationContext context, Func<Task> next);
}
