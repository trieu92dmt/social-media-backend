using IdentityService.Application.Abstractions.Security;

namespace IdentityService.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(
            password);
    }
}
