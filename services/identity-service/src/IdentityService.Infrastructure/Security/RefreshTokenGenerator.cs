using System.Security.Cryptography;

using IdentityService.Application.Abstractions.Security;

namespace IdentityService.Infrastructure.Security;

public class RefreshTokenGenerator
    : IRefreshTokenGenerator
{
    public string Generate()
    {
        var bytes = RandomNumberGenerator
            .GetBytes(64);

        return Convert.ToBase64String(bytes);
    }
}