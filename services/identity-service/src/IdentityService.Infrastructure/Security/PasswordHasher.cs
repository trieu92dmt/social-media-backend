using IdentityService.Application.Interfaces;

namespace IdentityService.Infrastructure.Security;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(
            password);
    }
}
