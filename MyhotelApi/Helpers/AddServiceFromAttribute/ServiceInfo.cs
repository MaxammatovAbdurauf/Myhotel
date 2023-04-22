namespace MyhotelApi.Helpers.AddServiceFromAttribute;

public class ServiceInfo
{
    public Type? type { get; set; }
    public List<Type>? baseTypes { get; set; }
    public ELifeTime lifeTime { get; set; }
}

public enum ELifeTime
{
    Transient,
    Scoped,
    Singleton
}