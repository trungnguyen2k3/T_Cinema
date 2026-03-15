using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class SysType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual ICollection<TblMovieType> TblMovieTypes { get; set; } = new List<TblMovieType>();
}
