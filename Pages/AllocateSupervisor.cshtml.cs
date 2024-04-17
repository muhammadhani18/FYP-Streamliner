using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace SE_Project.Pages
{
    public class AllocateSupervisorModel : PageModel
    {
        public List<string>? AvailableSupervisors { get; set; }
        public IActionResult OnPostSendEmail()
        {
            string message_to = Request.Form["message"];
            Console.WriteLine(message_to);
            try
            {
                // Gmail SMTP server details
                string smtpServer = "smtp.gmail.com";
                int port = 587;

                // Sender's email address and password
                string senderEmail = "mhani18403@gmail.com";
                string password = "qcwm fjuy spqg ongy"; // If using 2-Step Verification, generate an app password

                // Recipient's email address
                string recipientEmail = "i212595@nu.edu.pk";

                // Set up SMTP client and credentials
                SmtpClient client = new SmtpClient(smtpServer)
                {
                    Port = port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, password),
                    EnableSsl = true
                };

                // Create the email message
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "FYP Supervisor Request",
                    Body = message_to
                };

                // Add recipients
                message.To.Add(recipientEmail);

                // Send the email
                client.Send(message);

                // Email sent successfully
                return RedirectToPage("/Index"); // Redirect to a success page
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
                return Page(); // Return to the same page
            }
        }
        public IActionResult OnPostAllocateSupervisor()
        {
            string username = Request.Form["username"];
            string supervisor = Request.Form["supervisor"];
            Console.WriteLine("Username: " + username);
            Console.WriteLine("Supervisor: " + supervisor);


            try
            {
                string connectionString = "Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;";

                // Get FacultyNumber based on the selected supervisor's username
                string facultyNumberQuery = @"SELECT FacultyNumber 
                                              FROM Supervisors AS s 
                                              INNER JOIN Users AS u ON s.UserID = u.UserID 
                                              WHERE u.UserName = @supervisor";

                string facultyNumber;
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(facultyNumberQuery, connection))
                    {
                        command.Parameters.AddWithValue("@supervisor", supervisor);
                        facultyNumber = command.ExecuteScalar().ToString();
                    }
                }
                Console.WriteLine("Number: ", facultyNumber);


                // Get StudentID based on the provided username
                string studentIdQuery = @"SELECT s.RollNumber 
                                          FROM Users AS u 
                                          INNER JOIN Students AS s ON u.UserID = s.UserID 
                                          WHERE u.UserName = @username";

                string studentId;
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(studentIdQuery, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        studentId = command.ExecuteScalar().ToString();
                    }
                }

                // Insert the allocation request into the Requests table
                string insertQuery = "INSERT INTO Requests (FacultyNumber, StudentID) VALUES (@facultyNumber, @studentId)";
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@facultyNumber", facultyNumber);
                        command.Parameters.AddWithValue("@studentId", studentId);
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("./Success"); // Redirect to a success page
            }
            catch (Exception ex)
            {
                return BadRequest($"Error allocating supervisor: {ex.Message}"); // Return error status with message
            }
        }
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
