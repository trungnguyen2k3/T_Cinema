using System.ComponentModel.DataAnnotations;

namespace CinemaBE.Dtos.Accounts
{
    public class UpdateAccountResponseDto
    {
        public string? FullName { get; set; }
        
        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Dob { get; set; }
    }
}
