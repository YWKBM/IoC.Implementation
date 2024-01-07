namespace IoC.Implementation;

public interface IServiceProvider
{
    T GetService<T>();
    
    object GetServiceType(Type type);
}
