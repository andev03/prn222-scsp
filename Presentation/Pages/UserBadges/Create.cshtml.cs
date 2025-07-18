using BusinessLogic.IServices;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Pages.UserBadges
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public UserBadge Item { get; set; } = new UserBadge();
        public List<SelectListItem> UserList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BadgeList { get; set; } = new List<SelectListItem>();

        private readonly IUserBadgeService _userBadgeService;
        private readonly IUserService _userService;
        private readonly IBadgeService _badgeService;

        public CreateModel(IUserBadgeService userBadgeService, IUserService userService, IBadgeService badgeService)
        {
            _userBadgeService = userBadgeService;
            _userService = userService;
            _badgeService = badgeService;
        }

        public async Task OnGetAsync()
        {
            // Load users and badges for dropdowns
            await LoadBadges();

            // Initialize default values
            Item = new UserBadge
            {
                AwardedAt = DateTime.Now,
                Disabled = false,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };
        }

        private async Task LoadBadges()
        {
            try
            {
                var badges = await _badgeService.GetAll();
                // Only load active badges
                var activeBadges = badges.Where(b => b.Disabled != true && b.IsDeleted != true).ToList();

                BadgeList = activeBadges.Select(b => new SelectListItem
                {
                    Value = b.BadgeId.ToString(),
                    Text = $"{b.Name} - {b.Code}"
                }).ToList();

                // Add default option
                BadgeList.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "Select a badge..."
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading badges: {ex.Message}";
                BadgeList = new List<SelectListItem>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload dropdowns in case of validation errors
            await LoadBadges();

            if (Item.BadgeId <= 0)
            {
                ModelState.AddModelError("Item.BadgeId", "Please select a badge.");
            }

            if (Item.AwardedAt == default(DateTime))
            {
                ModelState.AddModelError("Item.AwardedAt", "Award date is required.");
            }

            // Check if awarded date is in the future
            if (Item.AwardedAt > DateTime.Now)
            {
                ModelState.AddModelError("Item.AwardedAt", "Award date cannot be in the future.");
            }

            // Check for duplicate awards (same user + same badge)
            try
            {
                var existingAwards = await _userBadgeService.GetAll();
                var duplicateAward = existingAwards.FirstOrDefault(ub =>
                    ub.UserId == Item.UserId &&
                    ub.BadgeId == Item.BadgeId &&
                    ub.IsDeleted != true);

                if (duplicateAward != null)
                {
                    ModelState.AddModelError("", "This user has already been awarded this badge.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error checking for duplicate awards: {ex.Message}");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Set system fields
                Item.CreatedAt = DateTime.Now;
                Item.UpdatedAt = DateTime.Now;
                Item.IsDeleted = false;
                Item.DeletedAt = null;

                // Ensure Disabled has a value (default to false if null)
                if (!Item.Disabled.HasValue)
                {
                    Item.Disabled = false;
                }

                // Create user badge through service
                await _userBadgeService.Create(Item);

                // Get user and badge info for success message
                Guid userId = Guid.Parse(Item.UserId+""); // nếu UserId là string chứa Guid
                var user = await _userService.GetById(userId);
                var badge = await _badgeService.GetById(Item.BadgeId);

                string userEmail = user?.Email ?? "Unknown User";
                string badgeName = badge?.Name ?? "Unknown Badge";

                // Redirect back to index with success message
                TempData["SuccessMessage"] = $"Badge '{badgeName}' awarded to '{userEmail}' successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                ModelState.AddModelError("", $"Error awarding badge: {ex.Message}");
                return Page();
            }
        }
    }
}