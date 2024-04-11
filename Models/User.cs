using System;

namespace SE_Project.Models
{
    public class User
    {
        public int UserID { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CNIC { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
    }

    public enum UserType
    {
        Administrator,
        Supervisor,
        Student
    }
}
