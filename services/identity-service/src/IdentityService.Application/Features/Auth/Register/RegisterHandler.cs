using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using MediatR;

namespace IdentityService.Application
    .Features.Auth.Register;

public class RegisterHandler
    : IRequestHandler<
        RegisterCommand,
        Guid>
{
    private readonly IUserRepository
        _userRepository;

    private readonly IPasswordHasher
        _passwordHasher;

    public RegisterHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var exists =
            await _userRepository
                .ExistsByEmailAsync(
                    request.Email);

        if (exists)
        {
            throw new Exception(
                "Email already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Username = request.Username,
            PasswordHash =
                _passwordHasher.Hash(
                    request.Password),
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}
