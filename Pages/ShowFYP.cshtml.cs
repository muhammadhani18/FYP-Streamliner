using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SE_Project.Pages
{
    public class ShowFYPModel : PageModel
    {
        public List<FYP> FYPs { get; set; }

        public void OnGet()
        {
            FYPs = new List<FYP>();

            string connectionString = "Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "select ProjectName, Description, Status, FirstName, LastName from Projects as p inner join Supervisors as s on p.FacultyNumber=s.FacultyNumber inner join  Users as u on s.UserID=u.UserID;\r\n";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                FYPs.Add(new FYP
                                {
                                    ProjectName = reader.GetString("ProjectName"),
                                    Description = reader.GetString("Description"),
                                    Status = reader.GetString("Status"),
                                    FirstName = reader.GetString("FirstName"),
                                    LastName = reader.GetString("LastName")

                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }

    public class FYP
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
