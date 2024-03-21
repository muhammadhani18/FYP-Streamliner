using System;
using System.Collections.Generic;

namespace Bismillah.Modal;

public partial class Supervisor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int GroupId { get; set; }

    public int PanelId { get; set; }

    public virtual ICollection<FypGroup> FypGroups { get; set; } = new List<FypGroup>();

    public virtual FypGroup Group { get; set; } = null!;

    public virtual Panel Panel { get; set; } = null!;

    public virtual ICollection<Panel> Panels { get; set; } = new List<Panel>();
}
