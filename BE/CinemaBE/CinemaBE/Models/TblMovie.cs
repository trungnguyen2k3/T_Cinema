using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblMovie
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? Duration { get; set; }

    public int? AgeLimit { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public string? Director { get; set; }

    public string? Actor { get; set; }

    public string? Poster { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<TblMovieType> TblMovieTypes { get; set; } = new List<TblMovieType>();

    public virtual ICollection<TblShowTime> TblShowTimes { get; set; } = new List<TblShowTime>();
}
