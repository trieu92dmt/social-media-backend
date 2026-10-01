using MediatR;

namespace IdentityService.Application
    .Features.Auth.Login;

public record LoginCommand(
    string Email,
    string Password
) : IRequest<LoginResponse>;