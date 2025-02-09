using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.DTOs;
using MyMedCalendar.Services;

namespace MyMedCalendar.Pages.Drug
{
    public class CreateDrugModel : PageModel
    {
        private readonly IDrugService _drugService;

        [BindProperty]
        public CreateDrugDTO NewDrug { get; set; }

        public CreateDrugModel(IDrugService drugService)
        {
            _drugService = drugService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _drugService.AddDrugAsync(NewDrug);
            return RedirectToPage("/Patient/Main");
        }
    }
}
