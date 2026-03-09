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
            return  Ok(await _accountService.GetAccountsAsync());
        }
    }
}
