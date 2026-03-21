using CinemaBE.Dtos;
using CinemaBE.Models;

namespace CinemaBE.Services
{
    public interface IAccountService
    {
        public Task<IEnumerable<SysAccount>> GetAccountsAsync();
        public Task<SysAccountResponseDto> RegisterAccountAsync(SysAccountRegisterDto sysAccountRegisterDto);
    }
}
