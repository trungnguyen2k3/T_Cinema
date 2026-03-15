using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblComboDetail
{
    public int Id { get; set; }

    public int BookingId { get; set; }

    public int ComboId { get; set; }

    public int Quantity { get; set; }

    public virtual TblBooking Booking { get; set; } = null!;

    public virtual SysCombo Combo { get; set; } = null!;
}
