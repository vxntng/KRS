using System.ComponentModel.DataAnnotations;
using BusinessObjects.Models;
using DataAccess.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Client.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInputModel Input { get; set; }
        public string? UserName { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // This method handles GET requests to the page
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // If model validation fails, return the page with validation errors
                return Page();
            }

            // Validate login credentials
            var accountManagement = AccountManagement.Instance;
            var user = accountManagement.GetAccountByUserPass(Input.UserName, Input.Password);

            if (user == null)
            {
                // If login fails, set error message and return the page
                ErrorMessage = "Invalid username or password.";
                return Page();
            }

            // If login succeeds, redirect to the dashboard page
            return RedirectToPage("/Dashboard");
        }
    }

    public class LoginInputModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
