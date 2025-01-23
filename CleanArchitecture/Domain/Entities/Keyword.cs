using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Keyword
{
    public Guid Id { get; set; }

    public string Word { get; set; } = null!;

    public Guid? AutomationId { get; set; }

    public virtual Automation? Automation { get; set; }
}
