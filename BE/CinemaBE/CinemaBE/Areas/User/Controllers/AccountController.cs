using CinemaBE.Commons;
using CinemaBE.Dtos;
using CinemaBE.Helpers;
using CinemaBE.Services;
using Microsoft.AspNetCore.Mvc;

namespace CinemaBE.Areas.User.Controllers
{
    [Area("User")]
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _accountService.GetAccountsAsync();
            return  Ok(await _accountService.GetAccountsAsync());
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAccount()
        {
            try
            {
                var accounts = await _accountService.GetAccountsAsync();
                var result = accounts.Select(x => new
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = x.Role,
                    FullName = x.FullName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Gender = x.Gender,
                    Dob = x.Dob,
                    Status = x.Status,
                    CreateAt = x.CreateAt,
                    UpdateAt = x.UpdateAt,

                });
                return Ok(new
                {
                    success = true,
                    message = "Lấy All Account thành công",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] SysAccountRegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    ApiResponse<object>.ErrorResult(
                        "Dữ liệu không hợp lệ",
                        ModelStateHelper.GetErrors(ModelState)
                    )
                );
            }

            try
            {
                var result = await _accountService.RegisterAccountAsync(dto);
                return Ok(ApiResponse<SysAccountResponseDto>.SuccessResult(result, "Đăng ký tài khoản thành công"));
            }
            catch (AppException ex)
            {
                return StatusCode(ex.StatusCode,
                    ApiResponse<object>.ErrorResult(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500,
                    ApiResponse<object>.ErrorResult("Lỗi hệ thống"));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAccount(SysAccountLoginRequestDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(
                    ApiResponse<object>.ErrorResult("Dữ liệu không hợp lệ", ModelStateHelper.GetErrors(ModelState)));
            }
            try
            {
                var result = await _accountService.LoginAccountAsync(dto);
                return Ok(result);
            }
            catch (AppException ex)
            {
                return StatusCode(
               ex.StatusCode,
               ApiResponse<object>.ErrorResult(ex.Message));
            }
            catch (Exception ex) {
                return StatusCode(
                500,
                ApiResponse<object>.ErrorResult("Lỗi hệ thống")
            );
            }
        }
    }
}
