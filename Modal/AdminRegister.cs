using System;
using System.Collections.Generic;

namespace Bismillah.Modal;

public partial class AdminRegister
{
    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<FypGroup> FypGroups { get; set; } = new List<FypGroup>();

    public virtual ICollection<Panel> Panels { get; set; } = new List<Panel>();
}
