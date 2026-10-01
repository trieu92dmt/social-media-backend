using Xunit;
using Moq;
using IdentityService.Application.Features.Auth.Login;
using IdentityService.Domain.Entities;
using IdentityService.Application.Interfaces;

namespace IdentityService.Application.UnitTests.Features.Auth.Login
{
    public class LoginHandlerTests
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<IRefreshTokenGenerator> _mockRefreshTokenGenerator;
        private readonly Mock<IRefreshTokenRepository> _mockRefreshTokenRepository;
        private readonly Mock<IJwtProvider> _mockJwtProvider;
        private readonly LoginHandler _handler;

        public LoginHandlerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockJwtProvider = new Mock<IJwtProvider>();
            _mockRefreshTokenGenerator = new Mock<IRefreshTokenGenerator>();
            _mockRefreshTokenRepository = new Mock<IRefreshTokenRepository>();
            _handler = new LoginHandler(
                _mockUserRepository.Object,
                _mockJwtProvider.Object,
                _mockRefreshTokenGenerator.Object,
                _mockRefreshTokenRepository.Object
            );
        }

        #region Handler_Should_Throw_Exception_When_User_Is_Not_Exist
        [Fact]
        public async Task Handle_Should_Throw_Exception_When_User_Is_Not_Exist()
        {
            // Arrange
            var command = new LoginCommand("nonexistent@example.com", "password");
            _mockUserRepository
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => act());
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(command.Email), Times.Once);
            _mockJwtProvider.Verify(provider => provider.Generate(It.IsAny<User>()), Times.Never);
            _mockRefreshTokenGenerator.Verify(generator => generator.Generate(), Times.Never);
            _mockRefreshTokenRepository.Verify(repo => repo.AddAsync(It.IsAny<RefreshToken>()), Times.Never);
        }
        #endregion

        #region Handler_Should_Throw_Exception_When_Password_Is_Invalid
        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Password_Is_Invalid()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "wrongpassword");
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctpassword")
            };
            _mockUserRepository
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => act());
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(command.Email), Times.Once);
            _mockJwtProvider.Verify(provider => provider.Generate(It.IsAny<User>()), Times.Never);
            _mockRefreshTokenGenerator.Verify(generator => generator.Generate(), Times.Never);
            _mockRefreshTokenRepository.Verify(repo => repo.AddAsync(It.IsAny<RefreshToken>()), Times.Never);
        }
        #endregion

        #region Handle_Should_Return_Access_And_Refresh_Token_And_Save_RefreshToken_When_User_Is_Valid
        [Fact]
        public async Task Handle_Should_Return_Access_And_Refresh_Token_And_Save_RefreshToken_When_User_Is_Valid()
        {
            // Arrange
            var command = new LoginCommand("test@example.com", "correctpassword");
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = command.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctpassword")
            };
            _mockUserRepository
                .Setup(repo => repo.GetByEmailAsync(command.Email))
                .ReturnsAsync(user);
            _mockJwtProvider
                .Setup(provider => provider.Generate(user))
                .Returns("access_token");
            _mockRefreshTokenGenerator
                .Setup(generator => generator.Generate())
                .Returns("refresh_token");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockUserRepository.Verify(repo => repo.GetByEmailAsync(command.Email), Times.Once);
            _mockJwtProvider.Verify(
                provider => provider.Generate(It.Is<User>(
                    u => u.Id == user.Id &&
                    u.Email == user.Email &&
                    u.PasswordHash == user.PasswordHash
                    )), Times.Once);
            _mockRefreshTokenGenerator.Verify(generator => generator.Generate(), Times.Once);
            _mockRefreshTokenRepository.Verify(
                repo => repo.AddAsync(It.Is<RefreshToken>(
                    rt => rt.Token == "refresh_token" &&
                    rt.Id != Guid.Empty &&
                    rt.UserId == user.Id &&
                    rt.ExpiresAt > DateTime.UtcNow
                    )), Times.Once);
            Assert.Equal("access_token", result.AccessToken);
            Assert.Equal("refresh_token", result.RefreshToken);
        }
        #endregion
    }
}
