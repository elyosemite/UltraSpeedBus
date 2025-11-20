namespace UltraSpeedBus.Message;

/// <summary>
/// Executes middleware in order. Accepts a list of IMiddleware resolved from DI.
/// </summary>
public class DefaultPipeline
{
    private readonly IReadOnlyList<IMiddleware> _middlewares;
    public DefaultPipeline(IEnumerable<IMiddleware> middlewares)
    {
        _middlewares = middlewares is IReadOnlyList<IMiddleware> list ? list : new List<IMiddleware>(middlewares);
    }

    public Task ExecuteAsync(MessageInvocationContext context, Func<Task> finalHandler)
    {
        // Build the pipeline delegate chain
        Func<Task> next = finalHandler;

        for (int i = _middlewares.Count - 1; i >= 0; i--)
        {
            var current = _middlewares[i];
            var localNext = next;
            next = () => current.InvokeAsync(context, localNext);
        }

        return next();
    }
}
