using CinemaBE.Commons;
using CinemaBE.Dtos.Accounts;
using CinemaBE.Helpers;
using CinemaBE.Models;
using CinemaBE.Services;
using CinemaBE.Tests.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaBE.Tests.UnitTests.Services
{
    public class AccountServiceTests
    {
        private static SysAccount CreateAccount(
            string username = "admin",
            string password = "123456",
            string email = "admin@gmail.com",
            string phone = "0909000001",
            string role = "USER",
            bool status = true)
        {
            return new SysAccount
            {
                Username = username,
                Password = AccountHelper.HashPassword(password),
                Email = email,
                PhoneNumber = phone,
                Role = role,
                Status = status,
                FullName = "Admin Test"
            };
        }

        [Fact]
        public async Task GetAccountsAsync_WhenCalled_ShouldReturnAllAccounts()
        {
            using var db = TestDbFactory.Create();
            db.SysAccounts.AddRange(
                CreateAccount(username: "user1", email: "u1@gmail.com", phone: "0901"),
                CreateAccount(username: "user2", email: "u2@gmail.com", phone: "0902")
            );
            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var result = await service.GetAccountsAsync();

            result.Should().HaveCount(2);
            result.Select(x => x.Username).Should().Contain(new[] { "user1", "user2" });
        }

        //[Fact]
        //public async Task LoginAccountAsync_WithValidCredentials_ShouldReturnSuccessResponse()
        //{
        //    using var db = TestDbFactory.Create();
        //    db.SysAccounts.Add(CreateAccount());
        //    await db.SaveChangesAsync();

        //    var service = new AccountServiceImpl(db);
        //    var dto = new LoginRequestDto
        //    {
        //        Username = "admin",
        //        Password = "123456"
        //    };

        //    var result = await service.LoginAccountAsync(dto);

        //    result.Should().NotBeNull();
        //    result.Success.Should().BeTrue();
        //    result.Message.Should().Be("Đăng nhập thành công");
        //    result.Data.Should().NotBeNull();
        //    result.Data!.Username.Should().Be("admin");
        //    result.Data.Email.Should().Be("admin@gmail.com");
        //    result.Data.Role.Should().Be("USER");
        //    result.Data.Status.Should().BeTrue();
        //}

        [Fact]
        public async Task LoginAccountAsync_WhenUsernameNotFound_ShouldThrowAppException404()
        {
            using var db = TestDbFactory.Create();
            var service = new AccountServiceImpl(db);

            var dto = new LoginRequestDto
            {
                Username = "notfound",
                Password = "123456"
            };

            Func<Task> act = async () => await service.LoginAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("User name không tồn tại");
            ex.Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task LoginAccountAsync_WhenPasswordIsWrong_ShouldThrowAppException()
        {
            using var db = TestDbFactory.Create();
            db.SysAccounts.Add(CreateAccount());
            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);
            var dto = new LoginRequestDto
            {
                Username = "admin",
                Password = "wrong-password"
            };

            Func<Task> act = async () => await service.LoginAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("Sai mật khẩu");
        }

        [Fact]
        public async Task LoginAccountAsync_WhenAccountIsLocked_ShouldThrowAppException403()
        {
            using var db = TestDbFactory.Create();
            db.SysAccounts.Add(CreateAccount(status: false));
            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);
            var dto = new LoginRequestDto
            {
                Username = "admin",
                Password = "123456"
            };

            Func<Task> act = async () => await service.LoginAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("Tài khoản đã bị khóa");
            ex.Which.StatusCode.Should().Be(403);
        }

        [Fact]
        public async Task RegisterAccountAsync_WithValidData_ShouldCreateAccountSuccessfully()
        {
            using var db = TestDbFactory.Create();
            var service = new AccountServiceImpl(db);

            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909000009",
                Fullname = "New User"
            };

            var result = await service.RegisterAccountAsync(dto);

            result.Should().NotBeNull();
            result.Username.Should().Be("newuser");
            result.Email.Should().Be("newuser@gmail.com");
            result.Role.Should().Be("USER");
            result.Status.Should().BeTrue();

            var accountInDb = await db.SysAccounts.FirstOrDefaultAsync(x => x.Username == "newuser");
            accountInDb.Should().NotBeNull();
            accountInDb!.Password.Should().NotBe("123456");
            AccountHelper.VerifyPassword("123456", accountInDb.Password).Should().BeTrue();
        }

        [Fact]
        public async Task RegisterAccountAsync_WhenPasswordConfirmDoesNotMatch_ShouldThrowAppException()
        {
            using var db = TestDbFactory.Create();
            var service = new AccountServiceImpl(db);

            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "654321",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909000009"
            };

            Func<Task> act = async () => await service.RegisterAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("Mật khẩu xác nhận không khớp");
        }

        [Fact]
        public async Task RegisterAccountAsync_WhenUsernameAlreadyExists_ShouldThrowAppException()
        {
            using var db = TestDbFactory.Create();
            db.SysAccounts.Add(CreateAccount(username: "admin", email: "old@gmail.com", phone: "0908"));
            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new RegisterRequestDto
            {
                Username = "admin",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "new@gmail.com",
                PhoneNumber = "0909"
            };

            Func<Task> act = async () => await service.RegisterAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("Username đã tồn tại");
        }

        [Fact]
        public async Task RegisterAccountAsync_WhenEmailAlreadyExists_ShouldThrowAppException()
        {
            using var db = TestDbFactory.Create();
            db.SysAccounts.Add(CreateAccount(username: "olduser", email: "admin@gmail.com", phone: "0908"));
            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "admin@gmail.com",
                PhoneNumber = "0909"
            };

            Func<Task> act = async () => await service.RegisterAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("Email đã tồn tại");
        }

        [Fact]
        public async Task RegisterAccountAsync_WhenPhoneAlreadyExists_ShouldThrowAppException()
        {
            using var db = TestDbFactory.Create();
            db.SysAccounts.Add(CreateAccount(username: "olduser", email: "old@gmail.com", phone: "0909000001"));
            await db.SaveChangesAsync();

            var service = new AccountServiceImpl(db);

            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "new@gmail.com",
                PhoneNumber = "0909000001"
            };

            Func<Task> act = async () => await service.RegisterAccountAsync(dto);

            var ex = await act.Should().ThrowAsync<AppException>();
            ex.Which.Message.Should().Be("Số điện thoại đã tồn tại");
        }

        [Fact]
        public async Task RegisterAccountAsync_ShouldTrimUsernameAndNormalizeEmail()
        {
            using var db = TestDbFactory.Create();
            var service = new AccountServiceImpl(db);

            var dto = new RegisterRequestDto
            {
                Username = "  newuser  ",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "  NEWUSER@GMAIL.COM  ",
                PhoneNumber = "0909000010"
            };

            var result = await service.RegisterAccountAsync(dto);

            result.Username.Should().Be("newuser");
            result.Email.Should().Be("newuser@gmail.com");

            var accountInDb = await db.SysAccounts.FirstAsync();
            accountInDb.Username.Should().Be("newuser");
            accountInDb.Email.Should().Be("newuser@gmail.com");
        }
    }
}