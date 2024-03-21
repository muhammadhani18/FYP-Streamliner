using System;
using System.Collections.Generic;

namespace Bismillah.Modal;

public partial class FypProject
{
    public int GroupId { get; set; }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Details { get; set; } = null!;

    public string? Photo { get; set; }

    public int? Rating { get; set; }

    public int PanelId { get; set; }

    public virtual FypGroup Group { get; set; } = null!;

    public virtual Panel Panel { get; set; } = null!;
}
