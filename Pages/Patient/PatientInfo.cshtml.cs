using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;
using System.Security.Claims;

namespace MyMedCalendar.Pages.Patient
{
    [Authorize]
    public class PatientInfoModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IUserService _userService;

        public PatientInfoModel(IPatientService patientService, IUserService userService)
        {
            _patientService = patientService;
            _userService = userService;
        }

        [BindProperty]
        public PatientDTO Patient { get; set; } = new PatientDTO();

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Get the current user's ID
                string userId = await _userService.GetCurrentUserIdAsync();

                // Fetch the patient's info by userId
                var patient = await _patientService.GetPatientByUserIdAsync(userId);
                if (patient == null)
                {
                    ErrorMessage = "Patient record not found. Please complete your information.";
                    Patient = new PatientDTO(); // Load an empty form
                }
                else
                {
                    Patient = patient;
                }

                return Page();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToPage("/User/Login");
                }

                await _patientService.UpdatePatientInfoAsync(userId, Patient);

                // Redirect to the dashboard
                return RedirectToPage("/Patient/Main");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating patient info: {ex.Message}");
                return Page();
            }
        }
    }
}
