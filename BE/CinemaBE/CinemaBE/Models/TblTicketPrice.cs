using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblTicketPrice
{
    public int Id { get; set; }

    public string SeatType { get; set; } = null!;

    public int TimeSlotId { get; set; }

    public decimal Price { get; set; }

    public bool? Status { get; set; }

    public virtual TblTimeSlot TimeSlot { get; set; } = null!;
}
