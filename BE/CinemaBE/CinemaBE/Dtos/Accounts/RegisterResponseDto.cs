namespace CinemaBE.Dtos.Accounts
{
    public class RegisterResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string? FullName { get; set; }
        public DateOnly? Dob { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = null!;
        public bool? Status { get; set; }
    }
}
