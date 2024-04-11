using System;

namespace SE_Project.Models
{
    public class Supervisor
    {
        public int Id { get; set; }
        public string FacultyNumber { get; set; } // Primary key
        public int UserID { get; set; } // Foreign key
        public Department Department { get; set; }

        // Navigation property for the User
        public User User { get; set; }
    }

    public enum Department
    {
        Computing,
        ElectricalEngineering,
        Management
    }
}
