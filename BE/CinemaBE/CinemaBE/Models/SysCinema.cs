using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class SysCinema
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public string? District { get; set; }

    public string? Address { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<TblRoom> TblRooms { get; set; } = new List<TblRoom>();
}
