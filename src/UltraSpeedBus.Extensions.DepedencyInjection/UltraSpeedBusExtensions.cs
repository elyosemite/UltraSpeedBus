namespace UltraSpeedBus.Extensions.DepedencyInjection;

using Microsoft.Extensions.DependencyInjection;
using UltraSpeedBus.Abstractions.Contracts;
using UltraSpeedBus.Abstractions.Mediator;
using UltraSppedBus.Abstractions.Mediator;

public static class UltraSpeedBusExtensions
{
    public static IServiceCollection AddUltraSpeedBus( this IServiceCollection services)
    {
        services.AddSingleton<IMediator, UltraMediator>();

        services.AddSingleton<ISend>(sp => sp.GetRequiredService<IMediator>());
        services.AddSingleton<IPublish>(sp => sp.GetRequiredService<IMediator>());
        services.AddSingleton<IConsumerConnector>(sp => sp.GetRequiredService<IMediator>());
        services.AddSingleton<IConsumerRegister>(sp => sp.GetRequiredService<IMediator>());

        return services;
    }
}
