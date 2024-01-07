namespace IoC.Implementation;

internal enum ServiceLiftime
{
    Singleton,
    Transient
}

internal record ServiceInfo
{
    internal Type Type {get; }

    internal Type Implementation {get; }

    internal ServiceLiftime Liftime {get; }

    public ServiceInfo(Type type, Type implementation, ServiceLiftime liftime)
    {
        Type = type;
        Implementation = implementation;
        Liftime = liftime;
    }
} 

public class ServiceCollection
{
    private readonly Dictionary<Type, ServiceInfo> _services = new();

    //by realization
    public void AddSingleton<T>()
    {
        var type = typeof(T);

        AddSingleton(type, type);
    }

    public void AddSingleton<TBaseType, TImpl>() where TImpl : TBaseType
    {
        var baseType = typeof(TBaseType);
        var implementationType = typeof(TImpl);

        AddSingleton(baseType, implementationType);
    }

    public void AddTransient<T>()
    {
        var type = typeof(T);

        AddTransient(type, type);
    }

    public void AddTransient<TBaseType, TImpl>() where TImpl : TBaseType
    {
        var baseType = typeof(TBaseType);
        var implementationType = typeof(TImpl);

        AddTransient(baseType, implementationType);
    }

    public IServiceProvider Build()
    {
        return new ServiceProvider();
    }

    private void AddSingleton(Type type, Type implementation) =>
        AddService(type, implementation, ServiceLiftime.Singleton);
    

    private void AddTransient(Type type, Type implementation) =>
        AddService(type, implementation, ServiceLiftime.Transient);
    

    private void AddService(Type type, Type implementation, ServiceLiftime serviceLiftime) =>
        _services.TryAdd(type, new ServiceInfo(type, implementation, serviceLiftime));
    
}
