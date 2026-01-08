using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineCourse.Common.Dtos;
using OnlineCourse.Common.Settings;
using OnlineCourse.Data.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCourse.Service.Helpers;

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly JwtSetting _jwtSetting = configuration.GetSection("JwtSettings").Get<JwtSetting>()!;
    public TokenDto GenerateToken(User user, bool populateExp)
    {
        var key = Encoding.UTF32.GetBytes(_jwtSetting.Key);
        var signingKey = new SigningCredentials(new SymmetricSecurityKey(key), "HS256");

        var claims = new List<Claim>()
        {
            new (ClaimTypes.Name, user.Username!),
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.GivenName, $"{user.FirstName} {user.LastName}"),
            new (ClaimTypes.Role, $"{user.Role.FullName}"),
            new Claim("role_id", user.RoleId.ToString())
        };

        var security = new JwtSecurityToken(issuer: _jwtSetting.Issuer,
            audience: _jwtSetting.Audience,
            signingCredentials: signingKey,
            claims: claims,
            expires: DateTime.Now.AddHours(12));

        var accessToken = new JwtSecurityTokenHandler().WriteToken(security);


        if (populateExp)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddDays(7);
        }


        return new TokenDto(accessToken: accessToken, refreshToken: user.RefreshToken!);
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
    public Tuple<bool, string> ValidateAndGetUser(string accessToken)
    {
        var key = Encoding.UTF32.GetBytes(_jwtSetting.Key);

        var options = new TokenValidationParameters()
        {
            ValidIssuer = _jwtSetting.Issuer,
            ValidateIssuer = true,
            ValidAudience = _jwtSetting.Audience,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, options, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            return new(false, null);

        var username = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (string.IsNullOrEmpty(username))
            return new(false, null);

        return new(true, username);
    }
}
