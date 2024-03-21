using System;
using System.Collections.Generic;

namespace Bismillah.Modal;

public partial class Panel
{
    public int Id { get; set; }

    public int SupervisorId { get; set; }

    public int AdminId { get; set; }

    public virtual AdminRegister Admin { get; set; } = null!;

    public virtual ICollection<FypProject> FypProjects { get; set; } = new List<FypProject>();

    public virtual Supervisor Supervisor { get; set; } = null!;

    public virtual ICollection<Supervisor> Supervisors { get; set; } = new List<Supervisor>();
}
