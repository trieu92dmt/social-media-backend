using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Security;

public class JwtProvider
    : IJwtProvider
{
    private readonly IConfiguration _configuration;

    public JwtProvider(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Generate(User user)
    {
        var key =
            _configuration["Jwt:Key"]!;

        var issuer =
            _configuration["Jwt:Issuer"]!;

        var audience =
            _configuration["Jwt:Audience"]!;

        var expireMinutes =
            int.Parse(
                _configuration[
                    "Jwt:ExpireMinutes"]!);

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Email,
                user.Email),

            new Claim(
                ClaimTypes.Name,
                user.Username)
        };

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key));

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.UtcNow
                .AddMinutes(expireMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}