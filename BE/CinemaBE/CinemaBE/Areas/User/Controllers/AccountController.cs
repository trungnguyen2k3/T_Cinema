using CinemaBE.Commons;
using CinemaBE.Dtos.Accounts;
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
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterRequestDto dto)
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
                return Ok(ApiResponse<RegisterResponseDto>.SuccessResult(result, "Đăng ký tài khoản thành công"));
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
        public async Task<IActionResult> LoginAccount(LoginRequestDto dto)
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
