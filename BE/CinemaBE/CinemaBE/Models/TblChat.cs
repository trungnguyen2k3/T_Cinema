using System;
using System.Collections.Generic;

namespace CinemaBE.Models;

public partial class TblChat
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public string MessageType { get; set; } = null!;

    public bool IsRead { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ReadAt { get; set; }

    public virtual SysAccount Receiver { get; set; } = null!;

    public virtual SysAccount Sender { get; set; } = null!;
}
