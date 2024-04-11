using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SE_Project.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
