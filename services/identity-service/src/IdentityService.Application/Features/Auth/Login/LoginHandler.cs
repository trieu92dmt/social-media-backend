using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using MediatR;

namespace IdentityService.Application
    .Features.Auth.Login;

public class LoginHandler
    : IRequestHandler<
        LoginCommand,
        LoginResponse>
{
    private readonly IUserRepository
        _userRepository;

    private readonly IJwtProvider
        _jwtProvider;

    private readonly IRefreshTokenGenerator
        _refreshTokenGenerator;

    private readonly IRefreshTokenRepository
        _refreshTokenRepository;

    public LoginHandler(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<LoginResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user =
            await _userRepository
                .GetByEmailAsync(
                    request.Email);

        if (user is null)
        {
            throw new Exception(
                "Invalid credentials");
        }

        var valid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

        if (!valid)
        {
            throw new Exception(
                "Invalid credentials");
        }

        var accessToken = _jwtProvider.Generate(user);
        var refreshToken = _refreshTokenGenerator.Generate();

        var refreshTokens = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await _refreshTokenRepository.AddAsync(refreshTokens);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}