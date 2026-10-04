using IdentityService.Application.Abstractions.Messaging;
using MassTransit;

namespace IdentityService.Infrastructure.Messaging;

public class MassTransitMessagePublisher : IMessagePublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitMessagePublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(
        T message,
        CancellationToken cancellationToken = default)
    {
        return _publishEndpoint.Publish(
            message ?? throw new ArgumentNullException(nameof(message)),
            cancellationToken);
    }
}