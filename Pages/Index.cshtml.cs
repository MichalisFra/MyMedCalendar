using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyMedCalendar.Services;

namespace MyMedCalendar.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;

        public IndexModel(ILogger<IndexModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Patient/Main");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _userService.SignOutAsync();
            return RedirectToPage("/User/Login");
        }
    }
}
