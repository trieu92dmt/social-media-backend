using MediatR;

namespace IdentityService.Application
    .Features.Auth.Logout;

public record LogoutCommand(string RefreshToken)
    : IRequest;