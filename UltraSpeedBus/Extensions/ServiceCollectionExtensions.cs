using Microsoft.Extensions.DependencyInjection;
using UltraSpeedBus.Message;

namespace UltraSpeedBus.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Mediator and supporting types. Consumers must register their handlers separately,
    /// e.g. services.AddTransient<ICommandHandler<MyCommand>, MyCommandHandler>();
    /// </summary>
    public static IServiceCollection AddUltraSpeedBusMediator(this IServiceCollection services)
    {
        // Register pipeline middlewares (none by default), but the DefaultPipeline expects IEnumerable<IMiddleware>
        services.AddTransient<HandlerRegistry>();
        services.AddTransient(sp =>
        {
            var middlewares = sp.GetServices<IMiddleware>();
            return new DefaultPipeline(middlewares);
        });

        services.AddSingleton<UltraSpeedBusMediator>();
        // Expose mediator via interface or concrete type
        services.AddSingleton(sp => sp.GetRequiredService<UltraSpeedBusMediator>());

        return services;
    }

    /// <summary>
    /// Register a middleware implementation.
    /// </summary>
    public static IServiceCollection AddUltraSpeedBusMiddleware<TMiddleware>(this IServiceCollection services)
        where TMiddleware : class, IMiddleware
    {
        services.AddTransient<IMiddleware, TMiddleware>();
        return services;
    }
}