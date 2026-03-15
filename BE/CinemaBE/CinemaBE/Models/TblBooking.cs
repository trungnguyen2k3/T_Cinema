using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblBooking
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int ShowTimeId { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual SysAccount? Account { get; set; }

    public virtual TblShowTime ShowTime { get; set; } = null!;

    public virtual ICollection<TblBookingDetail> TblBookingDetails { get; set; } = new List<TblBookingDetail>();

    public virtual ICollection<TblComboDetail> TblComboDetails { get; set; } = new List<TblComboDetail>();

    public virtual ICollection<TblPayment> TblPayments { get; set; } = new List<TblPayment>();
}
