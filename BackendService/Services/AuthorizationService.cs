using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common;
using Common.Enums;
using EducationService.Dto;
using EducationService.Models;
using Microsoft.IdentityModel.Tokens;

namespace EducationService.Services;

public class AuthorizationService
{
    private readonly UserService userService;

    public AuthorizationService(UserService userService)
    {
        this.userService = userService;
    }

    public async Task<AuthResponseDto?> AuthByLoginPassword(string username, string password)
    {
        var user = await userService.GetByUsername(username);
        if (user is null || user.Password != Common.Common.PasswordHash(password))
        {
            return null;
        }

        return new AuthResponseDto { AccessToken = GenerateAccessToken(user) };
    }

    public async Task<AuthResponseDto?> Register(RegisterDto user)
    {
        if (await userService.CheckUserExist(user) != ValidationUserStatus.Success)
        {
            return null;
        }
        
        var userAdded = await userService.AddUser(user);
        return userAdded is null ? null : new AuthResponseDto { AccessToken = GenerateAccessToken(userAdded) };
    }
    
    private static string GenerateAccessToken(User user)
    {
        var claims = GetClaims(user.Id, user.Role);
        return GenerateJwtToken(claims, AuthOptions.LifeTimeAccessToken);
    }

    private static string GenerateJwtToken(List<Claim> claims, int timeExpire)
    {
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(timeExpire)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    private static List<Claim> GetClaims(int userId, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimType.Id.ToString(), userId.ToString()),
            new (ClaimsIdentity.DefaultRoleClaimType, role)
        };
        return claims;
    }
}
