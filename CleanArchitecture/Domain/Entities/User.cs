using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string ClerkId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Automation> Automations { get; set; } = new List<Automation>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public virtual Subscription? Subscription { get; set; }
}
