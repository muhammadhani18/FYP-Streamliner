using System;

namespace SE_Project.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectID { get; set; } // Primary key
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public ProjectStatus Status { get; set; } = ProjectStatus.Active; // Default value
        public string FacultyNumber { get; set; } // Foreign key

        // Navigation property for the Supervisor
        public Supervisor Supervisor { get; set; }
    }

    public enum ProjectStatus
    {
        Active,
        Inactive,
        Completed
    }
}
