using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblMovieType
{
    public int Id { get; set; }

    public int MovieId { get; set; }

    public int TypeId { get; set; }

    public virtual TblMovie Movie { get; set; } = null!;

    public virtual SysType Type { get; set; } = null!;
}
