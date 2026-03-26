using CinemaBE.Commons;
using CinemaBE.Dtos.Accounts;
using CinemaBE.Helpers;
using CinemaBE.Models;
using CinemaBE.Tests.IntegrationTests;
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

        public async Task<GetAccountResponseDto> DeleteAccountAsync(int id)
        {
            var account = await _db.SysAccounts.FindAsync(id);
            if (account == null || account.Status == false)
            {
                throw new AppException("Tài khoản không có hoặc đã bị xóa",404);
            }
            account.Status = false;
            await _db.SaveChangesAsync();
            var accounts = await _db.SysAccounts.Select(x => new GetAccountResponseDto
            {
                Id = x.Id,
                Role = x.Role,
                Username = x.Username,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Gender = x.Gender,
                Dob = x.Dob,
                Status = x.Status,
                CreateAt = x.CreateAt,
                UpdateAt = x.UpdateAt,
            }).FirstOrDefaultAsync();
            return accounts;

        }

        public async Task<IEnumerable<GetAccountResponseDto>> GetAccountsAsync()
        {
            var accounts = await _db.SysAccounts.Select(x => new GetAccountResponseDto
            {
                Id = x.Id,
                Role = x.Role,
                Username = x.Username,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Gender = x.Gender,
                Dob = x.Dob,
                Status = x.Status,
                CreateAt = x.CreateAt,
                UpdateAt = x.UpdateAt,
            }).ToListAsync();
           
            return accounts;
        }

        public async Task<GetAccountResponseDto> GetByIdAsync(int id)
        {
            
            var account = await _db.SysAccounts.Select(x => new GetAccountResponseDto
            {
                Id = x.Id,
                Role = x.Role,
                Username = x.Username,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Gender = x.Gender,
                Dob = x.Dob,
                Status = x.Status,
                CreateAt = x.CreateAt,
                UpdateAt = x.UpdateAt,
            }).Where(x=> x.Id == id).FirstOrDefaultAsync();

            if(account == null)
            {
                throw new AppException("Tài khoản không tồn tại", 404);
            }
            return account;
        }

        public async Task<LoginResponseDto> LoginAccountAsync(LoginRequestDto dto)
        {

            var account = await _db.SysAccounts.FirstOrDefaultAsync(x => x.Username == dto.Username);
            if (account == null)
            {
                throw new AppException("User name không tồn tại", 404);
            }
            if (!AccountHelper.VerifyPassword(dto.Password, account.Password))
            {
                throw new AppException("Sai mật khẩu");
            }
            if(account.Status == false)
            {
                throw new AppException("Tài khoản đã bị khóa",403);
            }
            var result = new LoginResponseDto
            {
                Id = account.Id,
                Username = account.Username,
                Role = account.Role,
                FullName = account.FullName,
                Email = account.Email,
                PhoneNumber = account.PhoneNumber,
                Gender = account.Gender,
                Dob = account.Dob,
                Status = account.Status,
                CreateAt = account.CreateAt,
                UpdateAt = account.UpdateAt,
            };
            return result;
            }

        public async Task<RegisterResponseDto> RegisterAccountAsync(RegisterRequestDto dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new AppException("Mật khẩu xác nhận không khớp");

            string username = dto.Username.Trim();
            string email = dto.Email.Trim().ToLower();
            string phone = dto.PhoneNumber!.Trim();

            if (await _db.SysAccounts.AnyAsync(x => x.Username == username))
                throw new AppException("Username đã tồn tại");

            if (await _db.SysAccounts.AnyAsync(x => x.Email == email))
                throw new AppException("Email đã tồn tại");

            if (await _db.SysAccounts.AnyAsync(x => x.PhoneNumber == phone))
                throw new AppException("Số điện thoại đã tồn tại");

            var account = new SysAccount
            {
                Username = username,
                Email = email,
                PhoneNumber = phone,
                Password = AccountHelper.HashPassword(dto.Password),
                FullName = dto.Fullname,
                Dob = dto.Dob,
                Role = "USER",
                Status = true
            };

            await _db.SysAccounts.AddAsync(account);
            await _db.SaveChangesAsync();

            return new RegisterResponseDto
            {
                Id = account.Id,
                Username = account.Username,
                Dob = account.Dob,
                Email = account.Email,
                FullName = dto.Fullname,
                PhoneNumber = account.PhoneNumber,
                Role = account.Role,
                Status = account.Status
            };
        }

        public async Task<UpdateAccountResponseDto> UpdateAccountAsync(UpdatetAccountRequestDto updatetAccountRequestDto)
        {
           var account = await _db.SysAccounts.Where(x => x.Id == updatetAccountRequestDto.Id).FirstOrDefaultAsync();
            if (account == null) {
                throw new AppException("Tài khoản không tồn tại", 404);
            }
            account.PhoneNumber = updatetAccountRequestDto.PhoneNumber;
            account.Password = AccountHelper.HashPassword(updatetAccountRequestDto.Password);
            account.FullName = updatetAccountRequestDto.FullName;
            account.Gender = updatetAccountRequestDto.Gender;
            account.Dob = updatetAccountRequestDto.Dob;
            _db.SaveChangesAsync();
            return new UpdateAccountResponseDto
            {
                FullName = account.FullName,
                Gender = account.Gender,
                PhoneNumber = account.PhoneNumber,
                Dob = account.Dob,
            };
        }
    }
}
