using IdentityService.Application.Abstractions.Repositories;

using MediatR;

namespace IdentityService.Application
    .Features.Auth.Logout;

public class LogoutHandler : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _repository;

    public LogoutHandler(IRefreshTokenRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        var token = await _repository
            .GetByTokenAsync(request.RefreshToken);

        if (token is null)
            return;

        token.IsRevoked = true;
        token.RevokedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync();
    }
}