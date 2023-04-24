using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using MyhotelApi.Objects.Options;
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

        if (roles.Contains(RoleType.All))  
        {
            if (userRole == RoleType.User  || userRole == RoleType.Manager ||
                userRole == RoleType.Owner || userRole == RoleType.Admin   ||
                userRole == RoleType.Creator) {  }
             else throw new UnauthorizedAccessException();
        }

        else if (roles.Contains(RoleType.User))
        {
            if (userRole != RoleType.User) throw new UnauthorizedAccessException();
        }

        else if (roles.Contains(RoleType.Manager))
        {
            if (userRole != RoleType.Manager) throw new UnauthorizedAccessException();
        }

        else if (roles.Contains(RoleType.Owner))
        {
            if (userRole != RoleType.Owner) throw new UnauthorizedAccessException();
        }

        else if (roles.Contains(RoleType.Admin))
        {
            if (userRole != RoleType.Admin) throw new UnauthorizedAccessException();
        }

        else if (roles.Contains(RoleType.Creator))
        {
            if (userRole != RoleType.Creator) throw new UnauthorizedAccessException();
        }

        else
        {
            throw new UnauthorizedAccessException();
        }

        base.OnActionExecuting(context);
    }
}