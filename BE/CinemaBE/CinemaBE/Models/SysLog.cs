using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class SysLog
{
    public int Id { get; set; }

    public string TableName { get; set; } = null!;

    public int ObjectId { get; set; }

    public string Action { get; set; } = null!;

    public string? BeforeData { get; set; }

    public string? AfterData { get; set; }

    public int? AccountId { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual SysAccount? Account { get; set; }
}
