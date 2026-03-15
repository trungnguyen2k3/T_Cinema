using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblBookingDetail
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int ShowTimeId { get; set; }

    public int SeatId { get; set; }

    public decimal Price { get; set; }

    public virtual TblBooking Booking { get; set; } = null!;

    public virtual TblSeat Seat { get; set; } = null!;

    public virtual TblShowTime ShowTime { get; set; } = null!;
}
