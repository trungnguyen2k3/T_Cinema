using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class SysAccount
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? FullName { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<SysLog> SysLogs { get; set; } = new List<SysLog>();

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();
}
