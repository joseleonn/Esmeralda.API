using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Post
{
    public Guid Id { get; set; }

    public string Postid { get; set; } = null!;

    public string? Caption { get; set; }

    public string Media { get; set; } = null!;

    public Guid? AutomationId { get; set; }

    public virtual Automation? Automation { get; set; }
}
