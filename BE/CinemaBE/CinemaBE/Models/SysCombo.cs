using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class SysCombo
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<TblComboDetail> TblComboDetails { get; set; } = new List<TblComboDetail>();
}
