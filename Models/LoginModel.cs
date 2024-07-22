using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementApplication.Models
{
        public class LoginModel : PageModel
        {
            [BindProperty]
            public LoginInputModel Input { get; set; }

            public string ReturnUrl { get; set; }

            public void OnGet(string returnUrl = null)
            {
                ReturnUrl = returnUrl;
            }

            public IActionResult OnPost()
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                // TODO: Add authentication logic here
                // Example: Check username and password against a database

                if (Input.Username == "admin" && Input.Password == "password") // Dummy check
                {
                    // Add authentication logic here
                    return RedirectToPage("/Index"); // Redirect to the home page or desired page
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        public class LoginInputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }

