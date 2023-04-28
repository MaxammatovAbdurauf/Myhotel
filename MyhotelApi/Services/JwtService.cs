using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyhotelApi.Helpers.AddServiceFromAttribute;
using MyhotelApi.Objects.Options;
using MyhotelApi.Services.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyhotelApi.Services;

[Scoped]
public class JwtService : IJwtService
{
    private readonly JwtTokenValidationParameters validationParameters;
    public JwtService(IOptions<JwtTokenValidationParameters> validationParameters)
    {
        this.validationParameters = validationParameters.Value;
    }

    public string GenerateToken(string userId, string role)
    {
        var keyByte = Encoding.UTF8.GetBytes(validationParameters.IssuerSigningKey!);

        var securityKey = new SigningCredentials(new SymmetricSecurityKey(keyByte), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer: validationParameters.ValidIssuer,
                audience: validationParameters.ValidAudience,

                claims: new Claim[]
                {
                    new Claim (ClaimTypes.NameIdentifier,userId),
                    new Claim (ClaimTypes.Role,role)
                },

                expires: DateTime.Now.AddDays(1),
                signingCredentials: securityKey);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? GetPrincipal(string token)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(token);

        if (jwtSecurityToken == null) return null;

        var keyByte = Encoding.UTF8.GetBytes(validationParameters.IssuerSigningKey!);

        TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
        {
            RequireExpirationTime = true,
            ValidateIssuer = true,
            ValidIssuer = validationParameters.ValidIssuer,
            ValidateAudience = true,
            ValidAudience = validationParameters.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(keyByte)
        };

        SecurityToken securityToken;

        ClaimsPrincipal claimsPrincipal = 
            jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        
        return claimsPrincipal;
    }
}