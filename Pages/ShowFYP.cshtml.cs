using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text.Json;

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

                    string query = "select ProjectName, Description, Status, FirstName, LastName, rating from Projects as p inner join Supervisors as s on p.FacultyNumber=s.FacultyNumber inner join  Users as u on s.UserID=u.UserID;\r\n";

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
                                    LastName = reader.GetString("LastName"),
                                    Rating = reader.GetInt32("Rating")

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

        public IActionResult UpdateRating(string projectName, int increment)
        {
            Console.WriteLine("Error updating");
            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    Console.Write("Connection made");
                    // Update the rating in the database
                    string query = "UPDATE Projects SET rating = rating + @increment WHERE ProjectName = @projectName";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@increment", increment);
                        command.Parameters.AddWithValue("@projectName", projectName);
                        command.ExecuteNonQuery();
                    }
                }

                return Ok(); // Return success status
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating");
                return BadRequest($"Error updating rating: {ex.Message}"); // Return error status with message
            }
        }

        public async Task<IActionResult> OnPostSubmitFeedback()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string requestBody = await reader.ReadToEndAsync();
                    FeedbackModel feedbackModel = JsonSerializer.Deserialize<FeedbackModel>(requestBody);

                    // Access properties of feedbackModel
                    string projectName = feedbackModel.ProjectName;
                    string feedback = feedbackModel.Feedback;

                    // Log the project name and feedback to the console
                    Console.WriteLine($"Project Name: {projectName}, Feedback: {feedback}");

                    // You can add code here to save the feedback to the database or perform other actions

                    return Ok(); // Return success status
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error submitting feedback");
                return BadRequest($"Error submitting feedback: {ex.Message}"); // Return error status with message
            }
        }
        public class FeedbackModel
        {
            public string ProjectName { get; set; }
            public string Feedback { get; set; }
        }
        private IActionResult Ok()
        {

            throw new NotImplementedException();

        }
    }

    public class FYP
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Rating { get; set; }
    }
}
