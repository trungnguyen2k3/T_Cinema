using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblPayment
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? TransactionCode { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual TblBooking Booking { get; set; } = null!;
}
