using CinemaBE.Commons;
using CinemaBE.Dtos.Accounts;
using CinemaBE.Models;

namespace CinemaBE.Services
{
    public interface IAccountService
    {
        public Task<IEnumerable<SysAccount>> GetAccountsAsync();
        public Task<RegisterResponseDto> RegisterAccountAsync(RegisterRequestDto sysAccountRegisterDto);
        public Task<ApiResponse<LoginResponseDto>> LoginAccountAsync(LoginRequestDto sysAccountLoginRequestDto);
    }
}
