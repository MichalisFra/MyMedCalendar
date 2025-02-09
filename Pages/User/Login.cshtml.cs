using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Exceptions;
using MyMedCalendar.Services;

namespace MyMedCalendar.Pages.User
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
            Login = new LoginDTO();
        }

        [BindProperty]
        public LoginDTO Login { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Login Page Loaded"); // Debugging code

            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _userService.SignInAsync(Login.Email, Login.Password);
                return RedirectToPage("/Patient/Main"); // Redirect to the homepage or dashboard after successful login
            }
            catch (EntityNotAuthorizedException)
            {
                ModelState.AddModelError(string.Empty, "Invalid Credentials. Please try again.");
                return Page();
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return Page();
            }
        }
    }
}
