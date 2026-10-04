using BuildingBlocks.Contracts.Identity;
using MassTransit;
using UserService.Application.Abstractions.Repositories;
using UserService.Domain.Entities;

namespace UserService.Application.Consumers;

public class UserRegisteredConsumer 
    : IConsumer<UserRegisteredEvent>
{
    private readonly IUserProfileRepository _userProfileRepository;

    public UserRegisteredConsumer(
        IUserProfileRepository userProfileRepository)
    {
        _userProfileRepository = userProfileRepository;
    }

    public async Task Consume(
        ConsumeContext<UserRegisteredEvent> context)
    {
        var message = context.Message;

        var userProfile = new UserProfile
        {
            Id = message.Id,
            FullName = message.FullName,
            DisplayName = message.DisplayName,
            DOB = message.DOB,
            Address = message.Address
        };

        await _userProfileRepository.AddAsync(userProfile);
    }
}