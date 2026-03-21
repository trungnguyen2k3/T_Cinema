using CinemaBE.Helpers;
using FluentAssertions;
using Xunit;

namespace CinemaBE.Tests
{
    public class AccountHelperTests
    {
        [Fact]
        public void HashPassword_ShouldReturnDifferentValueFromOriginalPassword()
        {
            // Arrange
            var password = "123Trung@";

            // Act
            var hash = AccountHelper.HashPassword(password);

            // Assert
            hash.Should().NotBeNullOrWhiteSpace();
            hash.Should().NotBe(password);
        }

        [Fact]
        public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
        {
            // Arrange
            var password = "123Trung@";
            var hash = AccountHelper.HashPassword(password);

            // Act
            var result = AccountHelper.VerifyPassword(password, hash);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyPassword_WithWrongPassword_ShouldReturnFalse()
        {
            // Arrange
            var password = "123Trung@";
            var wrongPassword = "SaiMatKhau@1";
            var hash = AccountHelper.HashPassword(password);

            // Act
            var result = AccountHelper.VerifyPassword(wrongPassword, hash);

            // Assert
            result.Should().BeFalse();
        }
    }
}