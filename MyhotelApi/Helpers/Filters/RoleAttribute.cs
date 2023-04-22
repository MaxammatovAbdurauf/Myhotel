using Microsoft.AspNetCore.Mvc;

namespace MyhotelApi.Helpers.Exceptions;

public class RoleAttribute : TypeFilterAttribute
{
    public RoleAttribute(string roles) : base(typeof(JwtAuthoriseAttribute))
    {
        var roleList = roles.Split(',').ToList();
        Arguments = new object[] { new List<string>(roleList) };
    }
}