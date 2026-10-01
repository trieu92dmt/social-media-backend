using IdentityService.Application.Features.Auth.Login;
using FluentValidation.TestHelper;
using Xunit;

namespace IdentityService.Application.UnitTests.Features.Auth.Login
{
    public class LoginValidatorTests
    {
        private readonly LoginValidator _validator;
        public LoginValidatorTests()
        {
            _validator = new LoginValidator();
        }

        #region Validator_Should_Fail_When_Email_Is_Empty
        [Fact]
        public void Validator_Should_Fail_When_Email_Is_Empty()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "",
                Password = "password"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
        #endregion

        #region Should_Have_Error_When_Email_Is_Invalid
        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "invalid-email",
                Password = "password"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
        #endregion

        #region Validator_Should_Fail_When_Password_Is_Empty
        [Fact]
        public void Validator_Should_Fail_When_Password_Is_Empty()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = ""
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
        #endregion

        #region Should_Not_Have_Error_When_Request_Is_Valid
        [Fact]
        public void Should_Not_Have_Error_When_Request_Is_Valid()
        {
            // Arrange
            var request = new LoginRequest
            {
                Email = "test@example.com",
                Password = "password"
            };

            // Act
            var result = _validator.TestValidate(request);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
        #endregion
    }
}
