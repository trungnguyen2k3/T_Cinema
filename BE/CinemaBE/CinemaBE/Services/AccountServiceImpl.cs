using CinemaBE.Commons;
using CinemaBE.Dtos;
using CinemaBE.Helpers;
using CinemaBE.Models;
using CinemaBE.Hubs; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

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

        public async Task<ApiResponse<SysAccountLoginResponseDto>> LoginAccountAsync(SysAccountLoginRequestDto dto)
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
            var result = new SysAccountLoginResponseDto
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
            return ApiResponse<SysAccountLoginResponseDto>.SuccessResult(result, "Đăng nhập thành công");
            }

        public async Task<SysAccountResponseDto> RegisterAccountAsync(SysAccountRegisterDto dto)
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

            return new SysAccountResponseDto
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
    }
}
