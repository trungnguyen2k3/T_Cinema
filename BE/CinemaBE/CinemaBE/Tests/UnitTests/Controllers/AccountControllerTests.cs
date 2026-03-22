using CinemaBE.Areas.User.Controllers;
using CinemaBE.Commons;
using CinemaBE.Dtos.Accounts;
using CinemaBE.Models;
using CinemaBE.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CinemaBE.Tests.UnitTests.Controllers
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _serviceMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _serviceMock = new Mock<IAccountService>();
            _controller = new AccountController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAccounts_WhenServiceReturnsData_ShouldReturnOk()
        {
            var accounts = new List<SysAccount>
            {
                new() { Id = 1, Username = "user1", Email = "u1@gmail.com", PhoneNumber = "0901", Role = "USER", Status = true },
                new() { Id = 2, Username = "user2", Email = "u2@gmail.com", PhoneNumber = "0902", Role = "USER", Status = true }
            };

            _serviceMock
                .Setup(x => x.GetAccountsAsync())
                .ReturnsAsync(accounts);

            var result = await _controller.GetAccounts();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(accounts);

            _serviceMock.Verify(x => x.GetAccountsAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterAccount_WhenModelStateInvalid_ShouldReturnBadRequest()
        {
            _controller.ModelState.AddModelError("Username", "Username is required");

            var dto = new RegisterRequestDto();

            var result = await _controller.RegisterAccount(dto);

            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task RegisterAccount_WhenServiceSucceeds_ShouldReturnOk()
        {
            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909"
            };

            var response = new RegisterResponseDto
            {
                Id = 1,
                Username = "newuser",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909",
                Role = "USER",
                Status = true
            };

            _serviceMock
                .Setup(x => x.RegisterAccountAsync(dto))
                .ReturnsAsync(response);

            var result = await _controller.RegisterAccount(dto);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);

            _serviceMock.Verify(x => x.RegisterAccountAsync(dto), Times.Once);
        }

        [Fact]
        public async Task RegisterAccount_WhenServiceThrowsAppException_ShouldReturnExpectedStatusCode()
        {
            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909"
            };

            _serviceMock
                .Setup(x => x.RegisterAccountAsync(dto))
                .ThrowsAsync(new AppException("Username đã tồn tại", 409));

            var result = await _controller.RegisterAccount(dto);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task RegisterAccount_WhenUnexpectedExceptionOccurs_ShouldReturn500()
        {
            var dto = new RegisterRequestDto
            {
                Username = "newuser",
                Password = "123456",
                ConfirmPassword = "123456",
                Email = "newuser@gmail.com",
                PhoneNumber = "0909"
            };

            _serviceMock
                .Setup(x => x.RegisterAccountAsync(dto))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.RegisterAccount(dto);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task LoginAccount_WhenModelStateInvalid_ShouldReturnBadRequest()
        {
            _controller.ModelState.AddModelError("Password", "Password is required");

            var dto = new LoginRequestDto();

            var result = await _controller.LoginAccount(dto);

            var badResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task LoginAccount_WhenServiceSucceeds_ShouldReturnOk()
        {
            var dto = new LoginRequestDto
            {
                Username = "admin",
                Password = "123456"
            };

            var serviceResponse = ApiResponse<LoginResponseDto>.SuccessResult(
                new LoginResponseDto
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "0909000001",
                    Role = "USER",
                    Status = true,
                    FullName = "Admin Test"
                },
                "Đăng nhập thành công"
            );

            _serviceMock
                .Setup(x => x.LoginAccountAsync(dto))
                .ReturnsAsync(serviceResponse);

            var result = await _controller.LoginAccount(dto);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(serviceResponse);

            _serviceMock.Verify(x => x.LoginAccountAsync(dto), Times.Once);
        }

        [Fact]
        public async Task LoginAccount_WhenServiceThrowsAppException_ShouldReturnExpectedStatusCode()
        {
            var dto = new LoginRequestDto
            {
                Username = "admin",
                Password = "wrong"
            };

            _serviceMock
                .Setup(x => x.LoginAccountAsync(dto))
                .ThrowsAsync(new AppException("Sai mật khẩu", 400));

            var result = await _controller.LoginAccount(dto);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task LoginAccount_WhenUnexpectedExceptionOccurs_ShouldReturn500()
        {
            var dto = new LoginRequestDto
            {
                Username = "admin",
                Password = "123456"
            };

            _serviceMock
                .Setup(x => x.LoginAccountAsync(dto))
                .ThrowsAsync(new Exception("System error"));

            var result = await _controller.LoginAccount(dto);

            var objectResult = result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(500);
        }
    }
}