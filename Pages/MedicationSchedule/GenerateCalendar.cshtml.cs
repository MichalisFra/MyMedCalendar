using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;

namespace MyMedCalendar.Pages.MedicationSchedule
{
    [Authorize]
    [IgnoreAntiforgeryToken]
    public class GenerateCalendarModel : PageModel
    {
        private readonly IMedicationService _medicationService;

        public GenerateCalendarModel(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        [BindProperty]
        public List<MedicationEventDTO> Events { get; set; } = new();

        public string SerializedEvents { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync([FromForm] int[] selectedSchedules)
        {
            if (selectedSchedules == null || !selectedSchedules.Any())
            {
                ModelState.AddModelError(string.Empty, "No schedules selected.");
                return Page();
            }

            Events = await _medicationService.GenerateCalendarEventsAsync(selectedSchedules);

            if (Events == null || !Events.Any())
            {
                Console.WriteLine("No events generated.");
            }
            else
            {
                Console.WriteLine($"Generated {Events.Count} events.");
            }

            // Transform events to FullCalendar format
            var calendarEvents = Events!.Select(evt => new
            {
                title = evt.Notes, // "Take 3mg of ABILIFY"
                start = evt.Date.ToString("yyyy-MM-dd"),
                end = evt.Date.ToString("yyyy-MM-dd"),
                allDay = true
            });

            SerializedEvents = System.Text.Json.JsonSerializer.Serialize(calendarEvents);

            return Page();
        }

    }
}
