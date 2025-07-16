using BusinessLogic.IServices;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Presentation.Pages.Badges
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IBadgeService _badgeService;
        public List<Badge> badges { get; set; }
        public IndexModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }
        public async Task OnGet()
        {
            badges = await _badgeService.GetAll();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                // Get badge to check if it exists
                var badge = await _badgeService.GetById(id);
                if (badge == null)
                {
                    TempData["ErrorMessage"] = "Badge not found.";
                    return RedirectToPage();
                }

                // Store badge name for success message
                string badgeName = badge.Name;

                // Delete badge through service
                await _badgeService.SoftDelete(id);

                TempData["SuccessMessage"] = $"Badge '{badgeName}' has been deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting badge: {ex.Message}";
            }

            return RedirectToPage();
        }
    }
}
