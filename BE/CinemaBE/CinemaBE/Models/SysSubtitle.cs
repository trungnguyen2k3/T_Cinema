using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class SysSubtitle
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual ICollection<TblShowTime> TblShowTimes { get; set; } = new List<TblShowTime>();
}
