using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblImage
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public int ObjectId { get; set; }

    public string Path { get; set; } = null!;

    public string? Name { get; set; }

    public int? SortOrder { get; set; }
}
