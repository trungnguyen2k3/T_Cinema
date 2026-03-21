using System.ComponentModel.DataAnnotations;

namespace CinemaBE.Dtos
{
    public class SysAccountRegisterDto
    {
        [Required(ErrorMessage ="Username không được để trống")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage ="Password không được để trống")]
        [MinLength(6, ErrorMessage ="Password tối thiểu 6 kí tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*#?&]).+$", ErrorMessage = "Password phải có chữ hoa, chữ thường, số và ký tự đặc biệt")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage ="Xác nhận mật khẩu không được để trống")]
        [Compare("Password",ErrorMessage ="Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; } = null!;
        public string? Fullname { get; set; }
        public DateOnly? Dob { get; set; }
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? PhoneNumber { get; set; }
    }
}
