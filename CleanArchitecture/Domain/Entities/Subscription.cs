using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Subscription
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    public string Plan { get; set; }

    public string? CustomerId { get; set; }

    public virtual User? User { get; set; }
}
