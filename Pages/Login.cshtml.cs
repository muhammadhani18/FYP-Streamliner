using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;

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

            // Regular expressions for alphanumeric username and password with symbols
            string usernamePattern = "^[a-zA-Z0-9]*$";
            string passwordPattern = "^[a-zA-Z0-9!@#$%^&*()-_=+`~]*$";

            // Check if username matches alphanumeric pattern
            if (!Regex.IsMatch(username, usernamePattern))
            {
                Console.WriteLine("Username should contain only alphanumeric characters.");
                TempData["ErrorMessage"] = "Username should contain only alphanumeric characters.";
                return Page();
            }

            // Check if password matches alphanumeric with symbols pattern
            if (!Regex.IsMatch(password, passwordPattern))
            {
                Console.WriteLine("Password should contain only alphanumeric characters and symbols.");
                TempData["ErrorMessage"] = "Password should contain alphanumeric characters and symbols.";
                return Page();
            }

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
