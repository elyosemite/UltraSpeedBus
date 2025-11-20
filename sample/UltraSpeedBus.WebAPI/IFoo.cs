namespace UltraSpeedBus.WebAPI;

public interface IFoo
{
    string Does();
}

public class Foo : IFoo
{
    public string Does()
    {
        return "Quente";
    }
}

public interface IFeatureManager
{
    bool IsEnabled(string featureName);
}

public class FeatureManager : IFeatureManager
{
    public FeatureManager(IExternalScopeProvider scopeProvider)
    {
        
    }
    public bool IsEnabled(string featureName)
    {
        return true;
    }
}


public static class FooExtensions
{
    public static IServiceCollection AddFooService(this IServiceCollection service)
    {
        service.AddScoped<IFoo, Foo>();
        return service;
    }
}