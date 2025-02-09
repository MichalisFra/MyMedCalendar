using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;
using System.Security.Claims;

namespace MyMedCalendar.Pages.MedicationSchedule
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public EditModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [BindProperty]
        public MedicationScheduleDTO MedicationSchedule { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the schedule by its ID
            var schedule = await _medicationService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound($"No medication schedule found with ID {id}.");
            }

            // Ensure the user owns the schedule
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (schedule.PatientId != userId)
            {
                return Forbid(); // Prevent unauthorized access
            }

            MedicationSchedule = schedule;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Fetch the existing schedule
                var existingSchedule = await _medicationService.GetScheduleByIdAsync(MedicationSchedule.Id);
                if (existingSchedule == null)
                {
                    return NotFound($"Medication schedule with ID {MedicationSchedule.Id} not found.");
                }

                // Update the properties that are editable
                existingSchedule.Dosage = MedicationSchedule.Dosage;
                existingSchedule.FrequencyPerWeek = MedicationSchedule.FrequencyPerWeek;
                existingSchedule.StartDate = MedicationSchedule.StartDate;
                existingSchedule.EndDate = MedicationSchedule.EndDate;

                // Call service to update the schedule
                await _medicationService.UpdateScheduleAsync(existingSchedule);

                return RedirectToPage("/Patient/Main");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating schedule: {ex.Message}");
                ModelState.AddModelError(string.Empty, $"Error updating schedule: {ex.Message}");
                return Page();
            }
        }

    }

}




