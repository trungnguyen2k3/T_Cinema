using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblRoom
{
    public int Id { get; set; }

    public int CinemaId { get; set; }

    public string Name { get; set; } = null!;

    public string? RoomType { get; set; }

    public int Capacity { get; set; }

    public bool? Status { get; set; }

    public virtual SysCinema Cinema { get; set; } = null!;

    public virtual ICollection<TblSeat> TblSeats { get; set; } = new List<TblSeat>();

    public virtual ICollection<TblShowTime> TblShowTimes { get; set; } = new List<TblShowTime>();
}
