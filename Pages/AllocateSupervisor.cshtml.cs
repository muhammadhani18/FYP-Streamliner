using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace SE_Project.Pages
{
    public class AllocateSupervisorModel : PageModel
    {
        public List<string> AvailableSupervisors { get; set; }

        public void OnGet()
        {
            AvailableSupervisors = GetAvailableSupervisors();
        }

        private List<string> GetAvailableSupervisors()
        {
            List<string> supervisors = new List<string>();

            string connectionString = "Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;";
            string query = @"SELECT UserName
                             FROM Projects AS p 
                             INNER JOIN Supervisors AS s ON p.FacultyNumber = s.FacultyNumber 
                             INNER JOIN Users AS u ON u.UserID = s.UserID 
                             GROUP BY UserName 
                             HAVING COUNT(UserName) < 9;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                supervisors.Add(reader.GetString("UserName"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine("An error occurred while retrieving available supervisors: " + ex.Message);
            }

            return supervisors;
        }
    }
}
