using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Automation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool Active { get; set; }

    public Guid? UserId { get; set; }

    public virtual ICollection<Dm> Dms { get; set; } = new List<Dm>();

    public virtual ICollection<Keyword> Keywords { get; set; } = new List<Keyword>();

    public virtual Listener? Listener { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Trigger> Triggers { get; set; } = new List<Trigger>();

    public virtual User? User { get; set; }
}
