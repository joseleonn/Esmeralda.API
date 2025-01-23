using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Integration
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? UserId { get; set; }

    public string Token { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public string? InstagramId { get; set; }

    public virtual User? User { get; set; }
}
