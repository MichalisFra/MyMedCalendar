using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;

namespace MyMedCalendar.Pages.User
{
    public class SignupModel : PageModel
    {
        private readonly IUserService _userService;

       
        public SignupModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public SignupDTO Signup { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                await _userService.SignUpUserAsync(Signup);

                // Set a success message
                TempData["SuccessMessage"] = "Registration successful! Please complete your profile.";
                return RedirectToPage("/Patient/PatientInfo");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}
