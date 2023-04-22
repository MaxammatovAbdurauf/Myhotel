using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MyhotelApi.Middlewares;

public static class RequestCultureMiddleware
{
    public static void UseRequestCultureMiddleware(this WebApplication app)
    {
        app.UseRequestLocalization(options =>
        {
            options.DefaultRequestCulture = new RequestCulture("eng-US");
            options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("eng-US") };
            options.SupportedCultures = new List<CultureInfo> { new CultureInfo("eng-US") };
            options.RequestCultureProviders = new List<IRequestCultureProvider>();
        });
    }

   /* public async Task Invoke (HttpContext context)
    {
        if (context.Request.Query.ContainsKey ("lan"))
        {
            var culture = new CultureInfo("");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
        else if (context.Request.Headers.ContainsKey("lan"))
        {
            var culture = new CultureInfo("");
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }   
        await next(context);
    }*/
}