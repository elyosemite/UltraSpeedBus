using Microsoft.Extensions.DependencyInjection;
using UltraSpeedBus.Abstractions.Message;

namespace UltraSpeedBus.Message;

/// <summary>
/// Locates handlers from the configured DI container.
/// </summary>
public class HandlerRegistry
{
    private readonly IServiceProvider _serviceProvider;

    public HandlerRegistry(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Gets the single command handler for a command type.
    /// </summary>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public object? GetCommandHandler(Type commandType)
    {
        var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        return _serviceProvider.GetService(handlerType);
    }

    /// <summary>
    /// Gets the single query handler for the query type (queryType includes TResult generic).
    /// </summary>
    public object? GetQueryHandler(Type queryHandlerInterfaceType)
    {
        // queryHandlerInterfaceType is expected to be IQueryHandler<TQuery, TResult> closed generic
        return _serviceProvider.GetService(queryHandlerInterfaceType);
    }

    /// <summary>
    /// Gets all event handlers for an event type.
    /// </summary>
    public IEnumerable<object> GetEventHandlers(Type eventType)
    {
        var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);
        var services = _serviceProvider.GetServices(handlerType);
        
        if (services is not null) return services!;

        return Enumerable.Empty<object>();
    }
}
