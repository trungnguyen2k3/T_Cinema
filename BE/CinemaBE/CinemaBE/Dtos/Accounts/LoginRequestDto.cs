using System.ComponentModel.DataAnnotations;

namespace CinemaBE.Dtos.Accounts
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage ="Không được để trống username")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Không được để trống password")]
        [MinLength(6, ErrorMessage ="Mật khẩu ít nhất có 6 kí tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).+$",
        ErrorMessage = "Password phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string Password { get; set; }
    }
}
