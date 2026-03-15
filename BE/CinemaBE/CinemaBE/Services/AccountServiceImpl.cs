using CinemaBE.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaBE.Services
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly DatabaseContext _db;
        public AccountServiceImpl(DatabaseContext db)
        {
            _db = db; 
        }

        public async Task<IEnumerable<SysAccount>> GetAccountsAsync()
        {
            var accounts = _db.SysAccounts.ToListAsync();
            return await accounts;
        }
    }
}
