namespace IdentityService.Application.Abstractions.Security;

public interface IRefreshTokenGenerator
{
    string Generate();
}