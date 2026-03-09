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

        public async Task<IEnumerable<TblAccount>> GetAccountsAsync()
        {
            var accounts = _db.TblAccounts.ToListAsync();
            return await accounts;
        }
    }
}
