using Microsoft.AspNetCore.Mvc;

namespace MyhotelApi.Helpers.Exceptions;

public class RoleAttribute : TypeFilterAttribute
{
    public RoleAttribute(params string[] roles) : base(typeof(JwtAuthoriseAttribute))
    {
        var roleList = roles.ToList();
        Arguments = new object[] { new List<string>(roleList) };
    }
}