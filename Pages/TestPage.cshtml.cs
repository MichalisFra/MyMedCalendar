using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class TestPageModel : PageModel
{
    public IActionResult OnPost()
    {
        return Content("Antiforgery token validated successfully!");
    }
}
