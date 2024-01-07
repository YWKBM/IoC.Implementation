namespace IoC.Implementation;

internal class ServiceProvider : IServiceProvider
{
    public T GetService<T>()
    {
        throw new NotImplementedException();
    }

    public object GetServiceType(Type type)
    {
        throw new NotImplementedException();
    }
}