using IdentityService.Domain.Entities;

namespace IdentityService.Application.Abstractions.Security;

public interface IJwtProvider
{
    string Generate(User user);
}