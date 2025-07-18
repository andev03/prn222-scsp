using BusinessLogic.IServices;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Pages.UserBadges
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public UserBadge Item { get; set; } = new UserBadge();
        public List<SelectListItem> UserList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> BadgeList { get; set; } = new List<SelectListItem>();

        private readonly IUserBadgeService _userBadgeService;
        private readonly IUserService _userService;
        private readonly IBadgeService _badgeService;

        public EditModel(IUserBadgeService userBadgeService, IUserService userService, IBadgeService badgeService)
        {
            _userBadgeService = userBadgeService;
            _userService = userService;
            _badgeService = badgeService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                // Load users and badges for dropdowns
                await LoadUsers();
                await LoadBadges();

                // Get user badge by ID
                Item = await _userBadgeService.GetById(id);

                if (Item == null || Item.IsDeleted == true)
                {
                    TempData["ErrorMessage"] = "Badge award not found or has been deleted.";
                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading badge award: {ex.Message}";
                return RedirectToPage("./Index");
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                UserList = users.Select(u => new SelectListItem
                {
                    Value = u.UserId.ToString(),
                    Text = u.Email
                }).ToList();

                // Add default option
                UserList.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "Select a user..."
                });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading users: {ex.Message}";
                UserList = new List<SelectListItem>();
            }
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

            // Check for duplicate awards (same user + same badge, excluding current record)
            try
            {
                var existingAwards = await _userBadgeService.GetAll();
                var duplicateAward = existingAwards.FirstOrDefault(ub =>
                    ub.UserId == Item.UserId &&
                    ub.BadgeId == Item.BadgeId &&
                    ub.IsDeleted != true &&
                    ub.UserBadgeId != Item.UserBadgeId); // Exclude current record

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
                Item.UpdatedAt = DateTime.Now;

                // Ensure Disabled has a value (default to false if null)
                if (!Item.Disabled.HasValue)
                {
                    Item.Disabled = false;
                }

                // Update user badge through service
                await _userBadgeService.Update(Item);

                // Get user and badge info for success message
                var user = await _userService.GetById(Item.UserId);
                var badge = await _badgeService.GetById(Item.BadgeId);

                string userEmail = user?.Email ?? "Unknown User";
                string badgeName = badge?.Name ?? "Unknown Badge";

                // Redirect back to index with success message
                TempData["SuccessMessage"] = $"Badge award '{badgeName}' for '{userEmail}' updated successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                ModelState.AddModelError("", $"Error updating badge award: {ex.Message}");
                return Page();
            }
        }
    }
}