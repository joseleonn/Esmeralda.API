using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Dm
{
    public Guid Id { get; set; }

    public Guid? AutomationId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? SenderId { get; set; }

    public string? Reciever { get; set; }

    public string? Message { get; set; }

    public virtual Automation? Automation { get; set; }
}
