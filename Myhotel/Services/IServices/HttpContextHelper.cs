using Microsoft.AspNetCore.Http;
namespace Myhotel.Services.IServices;

public class HttpContextHelper
{
    public static IHttpContextAccessor httpContextAccessor;

    public static HttpContext Current => httpContextAccessor.HttpContext;

    public static void AddResponceHeader (string key, string value)
    {
        if (Current.Response.Headers.Keys.Contains(key) == true)
        {
            Current.Response.Headers.Remove(key);
        }
        Current.Response.Headers.Add("Access-Control-Expose-Headers", key);
        Current.Response.Headers.Add(key, value);
    }
}