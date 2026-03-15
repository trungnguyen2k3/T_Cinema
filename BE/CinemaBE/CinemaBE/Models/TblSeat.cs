using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblSeat
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public string SeatType { get; set; } = null!;

    public string? SeatRow { get; set; }

    public int? SeatNumber { get; set; }

    public string? Name { get; set; }

    public bool? Status { get; set; }

    public virtual TblRoom Room { get; set; } = null!;

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();
}
