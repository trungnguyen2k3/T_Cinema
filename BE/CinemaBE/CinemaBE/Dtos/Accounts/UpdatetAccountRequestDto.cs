using System.ComponentModel.DataAnnotations;

namespace CinemaBE.Dtos.Accounts
{
    public class UpdatetAccountRequestDto
    {
        public int Id { get; set; }
        [MinLength(6, ErrorMessage = "Password tối thiểu 6 kí tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).+$", ErrorMessage = "Password phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string? Password { get; set; } = null!;
        public string? FullName { get; set; }
        [Phone(ErrorMessage ="Số điện thoại chưa đúng định dạng")]
        public string? PhoneNumber { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Dob { get; set; }
    }
}
