using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace SE_Project.Pages
{
    public class AddFYPModel : PageModel
    {
        public IActionResult OnPost(IFormFile image)
        {
            string username = Request.Form["username"];
            Console.WriteLine(username);
         
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Login"); // Redirect to login page if username is not provided
            }

            string connectionString = "Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;";
            
            string title = Request.Form["title"];
            string supervisor_username = Request.Form["supervisor"];
            string details = Request.Form["details"];
            string status = Request.Form["status"];
            string team_member_1 = Request.Form["teamMember1"];
            string team_member_2 = Request.Form["teamMember2"];
            string team_member_3 = Request.Form["teamMember3"];
            string uniqueFileName = "";

            // Generate a unique ProjectID using GUID
            Guid projectID = Guid.NewGuid();
            Console.WriteLine("GUID: " + projectID);
            
            if (image != null && image.Length > 0)
            {
                try
                {
                    // Generate a unique filename using GUID
                    uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    Console.WriteLine(uniqueFileName);

                    // Combine the path to the uploads folder with the filename
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the uploaded file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }
             
                    // Log the success
                    Console.WriteLine("Image uploaded successfully.");
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    Console.WriteLine("An error occurred while uploading the image: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No image uploaded.");
            }
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Get FacultyNumber from Supervisor's username
                    string facultyQuery = "SELECT s.FacultyNumber FROM Supervisors AS s INNER JOIN Users AS u ON s.UserID = u.UserID WHERE u.UserName = @SupervisorUsername";
                    using (var facultyCommand = new MySqlCommand(facultyQuery, connection))
                    {
                        facultyCommand.Parameters.AddWithValue("@SupervisorUsername", supervisor_username);
                        string facultyNumber = facultyCommand.ExecuteScalar().ToString();

                        // Insert project details into Projects table
                        string insertQuery = "INSERT INTO Projects (ProjectID, ProjectName, Description, Status, FacultyNumber, image_path) VALUES (@ProjectID, @ProjectName, @Description, @Status, @FacultyNumber, @image_path)";
                        using (var insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@ProjectID", projectID);
                            insertCommand.Parameters.AddWithValue("@ProjectName", title);
                            insertCommand.Parameters.AddWithValue("@Description", details);
                            insertCommand.Parameters.AddWithValue("@Status", status);
                            insertCommand.Parameters.AddWithValue("@FacultyNumber", facultyNumber);
                            insertCommand.Parameters.AddWithValue("@image_path", uniqueFileName);

                            // Execute the insert query
                            int rowsAffected = insertCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                // Get RollNumber of logged-in user
                                string rollNumberQuery = "SELECT s.RollNumber FROM Students AS s INNER JOIN Users AS u ON s.UserID = u.UserID WHERE u.UserName = @Username";
                                using (var rollNumberCommand = new MySqlCommand(rollNumberQuery, connection))
                                {
                                    rollNumberCommand.Parameters.AddWithValue("@Username", username);
                                    try
                                    {
                                        string rollNumber = rollNumberCommand.ExecuteScalar().ToString();
                                        Console.WriteLine("RollNumber: " + rollNumber);

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("An error occurred: " + e.Message);
                                        Console.WriteLine("Failed to fetch roll number");
                                    }

                                    //string team_member_3 = username;

                                    

                                    // Update ProjectID for team members
                                    try
                                    {
                                        string updateQuery = "UPDATE Students SET ProjectID = @ProjectID WHERE RollNumber IN (@TeamMember1, @TeamMember2, @TeamMember3)";
                                        using (var updateCommand = new MySqlCommand(updateQuery, connection))
                                        {
                                            updateCommand.Parameters.AddWithValue("@ProjectID", projectID);
                                            updateCommand.Parameters.AddWithValue("@TeamMember1", team_member_1);
                                            updateCommand.Parameters.AddWithValue("@TeamMember2", team_member_2);
                                            updateCommand.Parameters.AddWithValue("@TeamMember3", team_member_3);

                                            // Execute the update query
                                            int updatedRows = updateCommand.ExecuteNonQuery();
                                            if (updatedRows > 0)
                                            {
                                                Console.WriteLine("UPDATED!!!!");
                                                Redirect("/");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error in updating 1");
                                            }
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error inserting into projects.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                TempData["ErrorMessage"] = "An error occurred while attempting to add the FYP. Please try again later.";
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // Insert failed, display error
            TempData["ErrorMessage"] = "Failed to add the FYP. Please try again later.";
            return Page();
        }
    }
}
