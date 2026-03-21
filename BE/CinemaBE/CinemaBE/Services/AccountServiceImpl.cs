using CinemaBE.Dtos;
using CinemaBE.Helpers;
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

        public async Task<SysAccountResponseDto> RegisterAccountAsync(SysAccountRegisterDto sysAccountRegisterDto)
        {
            if (sysAccountRegisterDto.Password != sysAccountRegisterDto.ConfirmPassword)
                throw new Exception("Mật khẩu xác nhận không khớp");

            string username = sysAccountRegisterDto.Username.Trim();
            string email = sysAccountRegisterDto.Email.Trim().ToLower();
            string phone = sysAccountRegisterDto.PhoneNumber!.Trim();

            if (await _db.SysAccounts.AnyAsync(x => x.Username == username))
                throw new Exception("Username đã tồn tại");

            if (await _db.SysAccounts.AnyAsync(x => x.Email == email))
                throw new Exception("Email đã tồn tại");

            if (await _db.SysAccounts.AnyAsync(x => x.PhoneNumber == phone))
                throw new Exception("Số điện thoại đã tồn tại");

            var account = new SysAccount
            {
                Username = username,
                Email = email,
                PhoneNumber = phone,
                Password = AccountHelper.HashPassword(sysAccountRegisterDto.Password),
                FullName = sysAccountRegisterDto.Fullname,
                Dob = sysAccountRegisterDto.Dob,
                Role = "User",
                Status = true
            };

            await _db.SysAccounts.AddAsync(account);
            await _db.SaveChangesAsync();

            return new SysAccountResponseDto
            {
                Id = account.Id,
                Username = account.Username,
                Dob = account.Dob,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                Role = account.Role,
                Status = account.Status
            };
        }
    }
}
