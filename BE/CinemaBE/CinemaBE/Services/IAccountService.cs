using CinemaBE.Models;

namespace CinemaBE.Services
{
    public interface IAccountService
    {
        public Task<IEnumerable<SysAccount>> GetAccountsAsync();
    }
}
