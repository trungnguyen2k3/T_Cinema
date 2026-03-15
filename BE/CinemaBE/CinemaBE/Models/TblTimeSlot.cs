using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblTimeSlot
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual ICollection<TblTicketPrice> TblTicketPrices { get; set; } = new List<TblTicketPrice>();
}
