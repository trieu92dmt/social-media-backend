using IdentityService.Application
    .Features.Auth.Login;

using MediatR;

namespace IdentityService.Application
    .Features.Auth.Refresh;

public record RefreshCommand(
    string RefreshToken
) : IRequest<LoginResponse>;