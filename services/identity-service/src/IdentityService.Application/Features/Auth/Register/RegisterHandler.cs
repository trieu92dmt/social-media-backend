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
    private readonly IRoleRepository 
        _roleRepository;
    private readonly IPasswordHasher
        _passwordHasher;

    public RegisterHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
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

        // Get the default role for new users
        var defaultRole = await _roleRepository.GetByCodeAsync(RoleConstants.User);

        var newUserId = Guid.NewGuid();

        var user = new User
        {
            Id = newUserId,
            Email = request.Email,
            Phone = request.Phone,
            Username = request.Username,
            PasswordHash =
                _passwordHasher.Hash(
                    request.Password),
            CreatedAt = DateTime.UtcNow,
            UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    RoleId = defaultRole?.Id ?? throw new Exception("Default role not found"),
                    UserId = newUserId
                }
            }
        };

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}
