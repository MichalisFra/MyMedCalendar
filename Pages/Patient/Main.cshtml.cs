using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;

namespace MyMedCalendar.Pages.Patient
{
    [Authorize]
    public class MainModel : PageModel
    {
        private readonly IMedicationService _medicationService;
        private readonly IUserService _userService;

        public MainModel(IMedicationService medicationService, IUserService userService)
        {
            _medicationService = medicationService;
            _userService = userService;
        }

        // Property to hold the fetched schedules
        public IEnumerable<MedicationScheduleDTO> MedicationSchedules { get; set; } = new List<MedicationScheduleDTO>();

        public async Task OnGetAsync()
        {
            // Retrieve the currently logged-in user's ID
            var userId = await _userService.GetCurrentUserIdAsync();
            if (string.IsNullOrEmpty(userId))
            {
                // Redirect to login if no user is logged in
                Response.Redirect("/User/Login");
                return;
            }

            // Fetch medication schedules using the user's ID
            MedicationSchedules = await _medicationService.GetSchedulesByUserIdAsync(userId);
        }
    }
}
