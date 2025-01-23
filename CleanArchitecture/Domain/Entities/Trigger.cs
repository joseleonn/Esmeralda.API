using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Trigger
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public Guid? AutomationId { get; set; }

    public virtual Automation? Automation { get; set; }
}
