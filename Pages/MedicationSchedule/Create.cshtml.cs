using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace MyMedCalendar.Pages.MedicationSchedule {

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMedicationService _medicationService;
        private readonly IDrugService _drugService;

        public CreateModel(IApplicationService applicationService)
        {
            _medicationService = applicationService.MedicationService;
            _drugService = applicationService.DrugService;
        }

        [BindProperty]
        public MedicationScheduleDTO MedicationSchedule { get; set; } = new();

        public SelectList DrugOptions { get; set; } = new SelectList(new List<string>());

        public async Task OnGetAsync()
        {
            try
            {
                var drugs = await _drugService.GetAllDrugsAsync();

                // If no drugs are found, initialize an empty SelectList
                DrugOptions = drugs != null && drugs.Any()
                    ? new SelectList(drugs, "Id", "Name")
                    : new SelectList(new List<string>()); // Empty SelectList
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching drugs: {ex.Message}");
                DrugOptions = new SelectList(new List<string>()); // Fallback to an empty list
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var drugs = await _drugService.GetAllDrugsAsync();
                DrugOptions = new SelectList(drugs, "Id", "Name");
                return Page();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError(string.Empty, "Unable to identify the current user.");
                return Page();
            }

            await _medicationService.AddScheduleAsync(MedicationSchedule, userId);
            return RedirectToPage("/Patient/Main");
        }
    }
}
