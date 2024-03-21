using System;
using System.Collections.Generic;

namespace Bismillah.Modal;

public partial class FypGroup
{
    public string Member1Name { get; set; } = null!;

    public string Member2Name { get; set; } = null!;

    public string Member3Name { get; set; } = null!;

    public string Supervisor { get; set; } = null!;

    public int Id { get; set; }

    public int SupervisorId { get; set; }

    public int AdminId { get; set; }

    public virtual AdminRegister Admin { get; set; } = null!;

    public virtual ICollection<FypProject> FypProjects { get; set; } = new List<FypProject>();

    public virtual Supervisor SupervisorNavigation { get; set; } = null!;

    public virtual ICollection<Supervisor> Supervisors { get; set; } = new List<Supervisor>();
}
