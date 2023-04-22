namespace MyhotelApi.Helpers.AddServiceFromAttribute;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ServiceAttribute : Attribute
{
    public ELifeTime lifeTime { get; set; }
    public ServiceAttribute() { }
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class ScopedAttribute : ServiceAttribute
{
    public ScopedAttribute()
    {
        base.lifeTime = ELifeTime.Scoped;
    }
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class TransientAttribute : ServiceAttribute
{
    public TransientAttribute()
    {
        base.lifeTime = ELifeTime.Scoped;
    }
}


[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SingletonAttribute : ServiceAttribute
{
    public SingletonAttribute()
    {
        base.lifeTime = ELifeTime.Scoped;
    }
}