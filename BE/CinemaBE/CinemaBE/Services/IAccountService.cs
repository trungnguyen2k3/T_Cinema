using CinemaBE.Commons;
using CinemaBE.Dtos.Accounts;
using CinemaBE.Models;

namespace CinemaBE.Services
{
    public interface IAccountService
    {
        public Task<IEnumerable<GetAccountResponseDto>> GetAccountsAsync();
        public Task<GetAccountResponseDto> GetByIdAsync(int id);
        public Task<RegisterResponseDto> RegisterAccountAsync(RegisterRequestDto sysAccountRegisterDto);
        public Task<LoginResponseDto> LoginAccountAsync(LoginRequestDto sysAccountLoginRequestDto);
        public Task<UpdateAccountResponseDto> UpdateAccountAsync(UpdatetAccountRequestDto updatetAccountRequestDto);
        public Task<GetAccountResponseDto> DeleteAccountAsync(int id);
    }
}
