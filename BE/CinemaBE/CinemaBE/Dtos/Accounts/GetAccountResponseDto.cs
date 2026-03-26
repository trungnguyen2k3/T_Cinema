using CinemaBE.Models;

namespace CinemaBE.Dtos.Accounts
{
    public class GetAccountResponseDto
    {
        public int Id { get; set; }

        public string Role { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string? FullName { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Dob { get; set; }

        public bool? Status { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

    }
}
