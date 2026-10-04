using MediatR;

namespace IdentityService.Application
    .Features.Auth.Register;

public record RegisterCommand(
    string Email,
    string Username,
    string Password,
    string ConfirmPassword,
    string Phone,
    string FullName,
    DateTime? DOB,
    string Address
) : IRequest<Guid>;