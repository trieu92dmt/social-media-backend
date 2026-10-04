using BuildingBlocks.Contracts.Identity;
using IdentityService.Application.Abstractions.Messaging;
using IdentityService.Application.Abstractions.Repositories;
using IdentityService.Application.Abstractions.Security;
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

    private readonly IMessagePublisher
        _messagePublisher;

    public RegisterHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IMessagePublisher messagePublisher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _messagePublisher = messagePublisher;
    }

    public async Task<Guid> Handle(
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        // Check if the email already exists
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
            Username = request.Username,
            PasswordHash =
                _passwordHasher.Hash(
                    request.Password),
            Email = request.Email,
            Phone = request.Phone,
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

        // Publish a message to RabbitMQ for the new user registration
        await _messagePublisher.PublishAsync(new UserRegisteredEvent
        {
            Id = newUserId,
            FullName = request.FullName,
            DisplayName = request.Username,
            DOB = request.DOB,
            Address = request.Address
        }, cancellationToken);

        await _userRepository.SaveChangesAsync();

        return user.Id;
    }
}
