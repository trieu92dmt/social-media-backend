using MediatR;

namespace IdentityService.Application
    .Features.Auth.Register;

public record RegisterCommand(
    string Email,
    string Username,
    string Password
) : IRequest<Guid>;