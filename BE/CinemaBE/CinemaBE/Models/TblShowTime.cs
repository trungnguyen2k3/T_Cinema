using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblShowTime
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int RoomId { get; set; }

    public int? SubtitleId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool? Status { get; set; }

    public virtual TblMovie Movie { get; set; } = null!;

    public virtual TblRoom Room { get; set; } = null!;

    public virtual SysSubtitle? Subtitle { get; set; }

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblBooking> TblBookings { get; set; } = new List<TblBooking>();
}
