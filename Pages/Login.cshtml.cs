using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;

namespace SE_Project.Pages
{
    public class LoginModel : PageModel
    {
        public IActionResult OnPost(string username, string password, string userType)
        {
            string connectionString = "Server=localhost;Port=3306;Database=FYP_Streamliner;Uid=root;Pwd=Deviljin1;";
            username = Request.Form["username"];
            password = Request.Form["password"];
            userType = Request.Form["userType"];

            Console.WriteLine("Username: " + username);
            Console.WriteLine("Password: " + password);
            Console.WriteLine("User Type: " + userType);

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute a query to fetch the user's information
                    string query = "SELECT * FROM Users WHERE UserName = @UserName";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Validate the user's credentials
                                string storedPassword = reader["Password"].ToString();

                                if (password == storedPassword)
                                {
                                    // Login successful
                                    return RedirectToPage("/Index");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                TempData["ErrorMessage"] = "An error occurred while attempting to login. Please try again later.";
                return Page();
            }

            // Login failed, display error
            TempData["ErrorMessage"] = "Invalid username or password.";
            return Page();
        }
    }
}
