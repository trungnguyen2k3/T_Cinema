using CinemaBE.Commons;
using CinemaBE.Dtos;
using CinemaBE.Helpers;
using CinemaBE.Models;
using CinemaBE.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaBE.Tests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task LoginAccountAsync_WithCorrectUsernameAndPassword_ShouldReturnSuccess()
        {
            // Arrange
            using var db = TestDbFactory.Create();

            db.SysAccounts.Add(new SysAccount
            {
                Username = "trung",
                Password = AccountHelper.HashPassword("123Trung@"),
                Role = "User",
                Email = "trung@gmail.com",
                PhoneNumber = "0912345678",
                Status = true
            });

            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new SysAccountLoginRequestDto
            {
                Username = "trung",
                Password = "123Trung@"
            };

            // Act
            var result = await service.LoginAccountAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Username.Should().Be("trung");
        }
        [Fact]
        public async Task LoginAccountAsync_WithWrongPassword_ShouldThrowAppException()
        {
            // Arrange
            using var db = TestDbFactory.Create();

            db.SysAccounts.Add(new SysAccount
            {
                Username = "trung",
                Password = AccountHelper.HashPassword("123Trung@"),
                Role = "User",
                Email = "trung@gmail.com",
                PhoneNumber = "0912345678",
                Status = true
            });

            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new SysAccountLoginRequestDto
            {
                Username = "trung",
                Password = "SaiPassword@1"
            };

            // Act
            Func<Task> act = async () => await service.LoginAccountAsync(dto);

            // Assert
            await act.Should().ThrowAsync<AppException>()
                .WithMessage("Sai mật khẩu");
        }
        [Fact]
        public async Task LoginAccountAsync_WhenUsernameNotFound_ShouldThrowAppException()
        {
            // Arrange
            using var db = TestDbFactory.Create();
            var service = new AccountServiceImpl(db);

            var dto = new SysAccountLoginRequestDto
            {
                Username = "khongtontai",
                Password = "123Trung@"
            };

            // Act
            Func<Task> act = async () => await service.LoginAccountAsync(dto);

            // Assert
            await act.Should().ThrowAsync<AppException>()
                .WithMessage("User name không tồn tại");
        }
        [Fact]
        public async Task LoginAccountAsync_WhenAccountIsLocked_ShouldThrowAppException()
        {
            // Arrange
            using var db = TestDbFactory.Create();

            db.SysAccounts.Add(new SysAccount
            {
                Username = "trung",
                Password = AccountHelper.HashPassword("123Trung@"),
                Role = "User",
                Email = "trung@gmail.com",
                PhoneNumber = "0912345678",
                Status = false
            });

            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new SysAccountLoginRequestDto
            {
                Username = "trung",
                Password = "123Trung@"
            };

            // Act
            Func<Task> act = async () => await service.LoginAccountAsync(dto);

            // Assert
            await act.Should().ThrowAsync<AppException>()
                .WithMessage("Tài khoản đã bị khóa");
        }
        [Fact]
        public async Task RegisterAccountAsync_WithValidData_ShouldCreateAccountSuccessfully()
        {
            // Arrange
            using var db = TestDbFactory.Create();
            var service = new AccountServiceImpl(db);

            var dto = new SysAccountRegisterDto
            {
                Username = "trung",
                Password = "123Trung@",
                ConfirmPassword = "123Trung@",
                Fullname = "Nguyen Trung",
                Dob = new DateOnly(2000, 1, 1),
                Email = "trung@gmail.com",
                PhoneNumber = "0912345678"
            };

            // Act
            var result = await service.RegisterAccountAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Username.Should().Be("trung");
            result.Email.Should().Be("trung@gmail.com");

            var accountInDb = await db.SysAccounts.FirstOrDefaultAsync(x => x.Username == "trung");
            accountInDb.Should().NotBeNull();
            accountInDb!.Password.Should().NotBe("123Trung@");
        }
        [Fact]
        public async Task RegisterAccountAsync_WhenUsernameAlreadyExists_ShouldThrowAppException()
        {
            // Arrange
            using var db = TestDbFactory.Create();

            db.SysAccounts.Add(new SysAccount
            {
                Username = "trung",
                Password = AccountHelper.HashPassword("123Trung@"),
                Role = "User",
                Email = "cu@gmail.com",
                PhoneNumber = "0900000001",
                Status = true
            });

            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new SysAccountRegisterDto
            {
                Username = "trung",
                Password = "123Trung@",
                ConfirmPassword = "123Trung@",
                Email = "moi@gmail.com",
                PhoneNumber = "0912345678"
            };

            // Act
            Func<Task> act = async () => await service.RegisterAccountAsync(dto);

            // Assert
            await act.Should().ThrowAsync<AppException>()
                .WithMessage("Username đã tồn tại");
        }
    }
}