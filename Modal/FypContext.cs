using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Bismillah.Modal;

public partial class FypContext : DbContext
{
    public FypContext()
    {
    }

    public FypContext(DbContextOptions<FypContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminRegister> AdminRegisters { get; set; }

    public virtual DbSet<FypGroup> FypGroups { get; set; }

    public virtual DbSet<FypProject> FypProjects { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Panel> Panels { get; set; }

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=mssql-166670-0.cloudclusters.net,10009;Database=fyp;User Id=admin;Password=Admin123.;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminRegister>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Admin_Re__3213E83FFEDBF338");

            entity.ToTable("Admin_Register");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FypGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FYP_Grou__3214EC27C387A2B3");

            entity.ToTable("FYP_Group");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AdminId).HasColumnName("Admin_id");
            entity.Property(e => e.Member1Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Member_1_Name");
            entity.Property(e => e.Member2Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Member_2_Name");
            entity.Property(e => e.Member3Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Member_3_Name");
            entity.Property(e => e.Supervisor)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SupervisorId).HasColumnName("Supervisor_Id");

            entity.HasOne(d => d.Admin).WithMany(p => p.FypGroups)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FYP_Group_Admin_Register");

            entity.HasOne(d => d.SupervisorNavigation).WithMany(p => p.FypGroups)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FYP_Group_Supervisor");
        });

        modelBuilder.Entity<FypProject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FYP_Proj__3214EC07348EB002");

            entity.ToTable("FYP_Project");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Details)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.GroupId).HasColumnName("Group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PanelId).HasColumnName("Panel_Id");
            entity.Property(e => e.Photo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Group).WithMany(p => p.FypProjects)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FYP_Project_FYP_Group");

            entity.HasOne(d => d.Panel).WithMany(p => p.FypProjects)
                .HasForeignKey(d => d.PanelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FYP_Project_Panel");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Login");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Panel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Panel__3214EC0738B3C878");

            entity.ToTable("Panel");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AdminId).HasColumnName("Admin_id");
            entity.Property(e => e.SupervisorId).HasColumnName("Supervisor_Id");

            entity.HasOne(d => d.Admin).WithMany(p => p.Panels)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Panel_Admin_Register");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Panels)
                .HasForeignKey(d => d.SupervisorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Panel_Supervisor");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Supervis__3214EC07CF9910A7");

            entity.ToTable("Supervisor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.GroupId).HasColumnName("Group_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PanelId).HasColumnName("Panel_Id");

            entity.HasOne(d => d.Group).WithMany(p => p.Supervisors)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supervisor_FYP_Group");

            entity.HasOne(d => d.Panel).WithMany(p => p.Supervisors)
                .HasForeignKey(d => d.PanelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Supervisor_Panel");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
