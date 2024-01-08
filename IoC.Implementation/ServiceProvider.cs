using System.Globalization;
using System.Reflection;

namespace IoC.Implementation;

internal class ServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, ServiceInfo> _services;
    private readonly Dictionary<Type, object> _singletons = new();

    public T GetService<T>()
    {
        var type = typeof(T);
        return (T) GetService(type);
    }

    

    public object GetService(Type type)
    {
        if (!_services.TryGetValue(type, out var servicesInfo))
            throw new TypeLoadException($"Не удалось зарезолвить {type.FullName}");
        
        if (servicesInfo.Liftime == ServiceLiftime.Singleton && _singletons.TryGetValue(type, out var instance))
            return instance;
        
        var constructors = type.GetConstructors();
        if (constructors.Length > 1)
            throw new TypeLoadException($"У типа {type.FullName} объявлено более 1 конструктора");

        var constructor = constructors.Single();
        var constructorParameters = constructor.GetParameters();


        return CreateInstance(type, servicesInfo, constructorParameters);
    }



    private object CreateInstance(Type type, ServiceInfo serviceInfo, ParameterInfo[] constructorParameters) 
    {
        
        var parametrInstances = new List<object>();
        
        //!!!
        foreach (var parametr in constructorParameters)
        {
            parametrInstances.Add(GetService(parametr.ParameterType));
        }

        var requiredInstance = parametrInstances.Count > 0 
            ? Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, parametrInstances.ToArray(), CultureInfo.CurrentCulture) 
            : Activator.CreateInstance(type);

        if (requiredInstance == null)
            throw new TypeLoadException($"Не удалось вызвать конструктор у типа {type.FullName}");
        
        if (serviceInfo.Liftime == ServiceLiftime.Singleton)
        {
            _singletons.TryAdd(type, requiredInstance);
        }

        return requiredInstance;
    }


    public ServiceProvider(Dictionary<Type, ServiceInfo> services)
    {
        _services = services;
    }
}