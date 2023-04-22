namespace MyhotelApi.Helpers.AddServiceFromAttribute;

public static class AddServicesViaAttribute
{
    public static void _AddServicesViaAttribute(this IServiceCollection builder_services)
    {
        IServiceCollection builder_services2 = builder_services;
        foreach (ServiceInfo service in ServiceCollector.GetServices())
        {
            switch (service.lifeTime)
            {
                case ELifeTime.Transient:
                    service.baseTypes!.ForEach(delegate (Type basetype)
                    {
                        builder_services2.AddTransient(basetype, service.type!);
                    });
                    break;

                case ELifeTime.Scoped:
                    service.baseTypes!.ForEach(delegate (Type basetype)
                    {
                        builder_services2.AddTransient(basetype, service.type!);
                    });
                    break;

                case ELifeTime.Singleton:
                    service.baseTypes!.ForEach(delegate (Type basetype)
                    {
                        builder_services2.AddTransient(basetype, service.type!);
                    });
                    break;
            }
        }
    }
}