using System.Reflection;

namespace MyhotelApi.Helpers.AddServiceFromAttribute;

public static class ServiceCollector
{
    //copy paste tushunmaganman
    public static List<ServiceInfo> GetServices()
    {
        return AppDomain.CurrentDomain.GetServicesFromDomain();
    }

    internal static List<ServiceInfo> GetServicesFromDomain(this AppDomain domain)
    {
        return domain.GetAssemblies()
        .SelectMany((Assembly a) => a.GetTypes()
        .Where(a_type => Filter1(a_type)))
        .Select(a_type => Filter2(a_type)).ToList();
    }

    internal static bool Filter1(Type type)
    {
        if (type.IsClass && !type.IsAbstract)
        {
            if (type.GetCustomAttribute<ServiceAttribute>() is not null)
            {
                return true;
            }
        }
        return false;
    }

    public static ServiceInfo Filter2(Type _type)
    {
        ServiceInfo serviceInfo = new ServiceInfo
        {
            type = _type,
            lifeTime = _type.GetCustomAttribute<ServiceAttribute>()!.lifeTime,
            baseTypes = new List<Type>()
        };
        //Add Intefaces:
        _type.GetInterfaces().ToList().ForEach(serviceInfo.baseTypes.Add);
        //Determine base type:
        Type baseType = _type!;
        // if base type of class is not object and it inherets from other class,
        // the we add parent class to base types::
        if (baseType != typeof(object))
        {
            serviceInfo.baseTypes.Add(baseType);
        }
        //basetypes ga class ni ozini qoshamiz:
        serviceInfo.baseTypes.Add(_type);
        return serviceInfo;
    }
}