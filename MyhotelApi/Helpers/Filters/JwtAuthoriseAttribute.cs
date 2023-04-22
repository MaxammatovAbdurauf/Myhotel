using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MyhotelApi.Services;
using System.Security.Claims;

namespace MyhotelApi.Helpers.Exceptions;

public class JwtAuthoriseAttribute : ActionFilterAttribute
{
    private readonly JwtService jwtService;
    private readonly List<string> roles;

    public JwtAuthoriseAttribute(JwtService jwtService, List<string> roles)
    {
        this.jwtService = jwtService;
        this.roles = roles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {

        if (!context.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var authorisationToken = context.HttpContext.Request.Headers[HeaderNames.Authorization];
        var claimPrincipal = jwtService.GetPrincipal(authorisationToken!);
        var userRole = claimPrincipal!.FindFirst(claim => claim.Type == ClaimTypes.Role)!.Value;

        if (roles.Contains("user"))
        {
        }
        else if (roles.Contains("admin"))
        {
            if (userRole != "admin" || userRole != "owner")
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        else if (roles.Contains("owner"))
        {
            if (userRole != "owner")
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
        else
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        base.OnActionExecuting(context);
    }
}