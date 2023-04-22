using System.Security.Claims;

namespace MyhotelApi.Services.IServices
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
        ClaimsPrincipal? GetPrincipal(string token);
    }
}