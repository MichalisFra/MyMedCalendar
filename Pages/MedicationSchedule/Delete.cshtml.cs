using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;
using System.Security.Claims;

namespace MyMedCalendar.Pages.MedicationSchedule
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public DeleteModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [BindProperty]
        public MedicationScheduleDTO MedicationSchedule { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var schedule = await _medicationService.GetSchedulesByPatientIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            MedicationSchedule = schedule.FirstOrDefault(s => s.Id == id)!;

            if (MedicationSchedule == null)
            {
                return NotFound($"No schedule found with ID {id}.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _medicationService.DeleteScheduleAsync(MedicationSchedule.Id);
                return RedirectToPage("/Patient/Main");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error deleting schedule: {ex.Message}");
                return Page();
            }
        }
    }

}
