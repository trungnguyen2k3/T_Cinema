using System.Net;
using System.Net.Http.Json;
using CinemaBE.Helpers;
using CinemaBE.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CinemaBE.Tests.IntegrationTests
{
    public class AccountApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public AccountApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAccounts_WhenDataExists_ShouldReturn200()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                db.SysAccounts.Add(new SysAccount
                {
                    Username = "user1",
                    Password = AccountHelper.HashPassword("123456"),
                    Email = "user1@gmail.com",
                    PhoneNumber = "0901",
                    Role = "USER",
                    Status = true
                });

                await db.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/account");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("user1");
        }

        [Fact]
        public async Task Login_WithValidCredentials_ShouldReturn200AndSuccessMessage()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                db.SysAccounts.Add(new SysAccount
                {
                    Username = "admin",
                    Password = AccountHelper.HashPassword("123456"),
                    Email = "admin@gmail.com",
                    PhoneNumber = "0909000001",
                    Role = "USER",
                    Status = true,
                    FullName = "Admin Test"
                });

                await db.SaveChangesAsync();
            }

            var request = new
            {
                Username = "admin",
                Password = "123456"
            };

            var response = await _client.PostAsJsonAsync("/api/account/login", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Đăng nhập thành công");
            content.Should().Contain("admin");
            content.Should().Contain("admin@gmail.com");
        }

        [Fact]
        public async Task Login_WithWrongPassword_ShouldReturn400()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                db.SysAccounts.Add(new SysAccount
                {
                    Username = "admin",
                    Password = AccountHelper.HashPassword("123456"),
                    Email = "admin@gmail.com",
                    PhoneNumber = "0909000001",
                    Role = "USER",
                    Status = true
                });

                await db.SaveChangesAsync();
            }

            var request = new
            {
                Username = "admin",
                Password = "wrong-password"
            };

            var response = await _client.PostAsJsonAsync("/api/account/login", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Sai mật khẩu");
        }

        [Fact]
        public async Task Login_WhenAccountLocked_ShouldReturn403()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                db.SysAccounts.Add(new SysAccount
                {
                    Username = "lockeduser",
                    Password = AccountHelper.HashPassword("123456"),
                    Email = "locked@gmail.com",
                    PhoneNumber = "0909000002",
                    Role = "USER",
                    Status = false
                });

                await db.SaveChangesAsync();
            }

            var request = new
            {
                Username = "lockeduser",
                Password = "123456"
            };

            var response = await _client.PostAsJsonAsync("/api/account/login", request);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Tài khoản đã bị khóa");
        }

        [Fact]
        public async Task Register_WithValidData_ShouldReturn200AndCreateAccount()
        {
            var request = new
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909000010",
                Fullname = "New User"
            };

            var response = await _client.PostAsJsonAsync("/api/account/register", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Đăng ký tài khoản thành công");
            content.Should().Contain("newuser");
            content.Should().Contain("newuser@gmail.com");
        }

        [Fact]
        public async Task Register_WhenUsernameExists_ShouldReturn400()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                db.SysAccounts.Add(new SysAccount
                {
                    Username = "existinguser",
                    Password = AccountHelper.HashPassword("123456"),
                    Email = "old@gmail.com",
                    PhoneNumber = "0909000099",
                    Role = "USER",
                    Status = true
                });

                await db.SaveChangesAsync();
            }

            var request = new
            {
                Username = "existinguser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "new@gmail.com",
                PhoneNumber = "0909000088",
                Fullname = "New User"
            };

            var response = await _client.PostAsJsonAsync("/api/account/register", request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Username đã tồn tại");
        }
    }
}