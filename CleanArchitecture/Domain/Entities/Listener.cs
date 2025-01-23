using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Listener
{
    public Guid Id { get; set; }

    public Guid AutomationId { get; set; }

    public string Prompt { get; set; } = null!;

    public string? CommentReply { get; set; }

    public int DmCount { get; set; }

    public int CommentCount { get; set; }

    public virtual Automation Automation { get; set; } = null!;
}
