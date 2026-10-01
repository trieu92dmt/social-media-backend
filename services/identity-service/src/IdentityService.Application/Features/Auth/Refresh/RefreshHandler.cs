using IdentityService.Application
    .Features.Auth.Login;
using IdentityService.Domain.Entities;
using IdentityService.Application.Interfaces;

using MediatR;

namespace IdentityService.Application
    .Features.Auth.Refresh;

public class RefreshHandler
    : IRequestHandler<
        RefreshCommand,
        LoginResponse>
{
    private readonly
        IRefreshTokenRepository
        _refreshTokenRepository;

    private readonly IUserRepository
        _userRepository;

    private readonly IJwtProvider
        _jwtProvider;

    private readonly IRefreshTokenGenerator
        _refreshTokenGenerator;


    public RefreshHandler(
        IRefreshTokenRepository
            refreshTokenRepository,
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _refreshTokenRepository =
            refreshTokenRepository;

        _userRepository =
            userRepository;

        _jwtProvider =
            jwtProvider;

        _refreshTokenGenerator =
            refreshTokenGenerator;
    }

    public async Task<LoginResponse>
        Handle(
            RefreshCommand request,
            CancellationToken cancellationToken)
    {
        var refreshToken =
            await _refreshTokenRepository
                .GetByTokenAsync(
                    request.RefreshToken);

        if (refreshToken is null)
        {
            throw new Exception(
                "Invalid refresh token");
        }

        if (refreshToken.IsRevoked)
        {
            throw new Exception(
                "Refresh token revoked");
        }

        if (refreshToken.ExpiresAt
            < DateTime.UtcNow)
        {
            throw new Exception(
                "Refresh token expired");
        }

        var user =
            await _userRepository
                .GetByIdAsync(
                    refreshToken.UserId);

        if (user is null)
        {
            throw new Exception(
                "User not found");
        }

        // Revoke the used refresh token
        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;

        await _refreshTokenRepository.SaveChangesAsync();

        // Generate new refresh token
        var newRefreshToken = _refreshTokenGenerator.Generate();

        // Save the new refresh token
        await _refreshTokenRepository.AddAsync(
        new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });

        return new LoginResponse
        {
            AccessToken =
                _jwtProvider.Generate(user),

            RefreshToken = newRefreshToken
        };
    }
}