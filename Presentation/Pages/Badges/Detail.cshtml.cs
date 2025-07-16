using BusinessLogic.IServices;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Badges
{
    public class DetailModel : PageModel
    {
        public Badge Item { get; set; } = new Badge();
        private readonly IBadgeService _badgeService;

        public DetailModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                // Get badge by ID
                Item = await _badgeService.GetById(id);

                if (Item == null)
                {
                    TempData["ErrorMessage"] = "Badge not found";
                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading badge: {ex.Message}";
                return RedirectToPage("./Index");
            }
        }
    }
}