using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblAccount
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Fullname { get; set; }

    public DateOnly? Dob { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}
