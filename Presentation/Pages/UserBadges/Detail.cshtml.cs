using BusinessLogic.IServices;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.UserBadges
{
    public class DetailModel : PageModel
    {
        public UserBadge Item { get; set; } = new UserBadge();
        private readonly IUserBadgeService _userBadgeService;

        public DetailModel(IUserBadgeService userBadgeService)
        {
            _userBadgeService = userBadgeService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
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

        // Helper methods for achievement analysis
        public string GetAwardStatus()
        {
            if (Item.Disabled == true)
                return "Disabled";
            else
                return "Active";
        }

        public string GetAwardStatusColor()
        {
            var status = GetAwardStatus();
            return status switch
            {
                "Active" => "success",
                "Disabled" => "warning",
                _ => "secondary"
            };
        }

        public string GetAwardStatusIcon()
        {
            var status = GetAwardStatus();
            return status switch
            {
                "Active" => "fas fa-check-circle",
                "Disabled" => "fas fa-pause-circle",
                _ => "fas fa-question-circle"
            };
        }

        public string GetTimeSinceAwarded()
        {
            var timeSpan = DateTime.Now - Item.AwardedAt;

            if (timeSpan.TotalDays >= 365)
                return $"{(int)(timeSpan.TotalDays / 365)} year(s) ago";
            else if (timeSpan.TotalDays >= 30)
                return $"{(int)(timeSpan.TotalDays / 30)} month(s) ago";
            else if (timeSpan.TotalDays >= 1)
                return $"{(int)timeSpan.TotalDays} day(s) ago";
            else if (timeSpan.TotalHours >= 1)
                return $"{(int)timeSpan.TotalHours} hour(s) ago";
            else
                return "Recently awarded";
        }

        public string GetAchievementCategory()
        {
            var daysSinceAwarded = (DateTime.Now - Item.AwardedAt).TotalDays;

            if (daysSinceAwarded <= 7)
                return "Recent Achievement";
            else if (daysSinceAwarded <= 30)
                return "Monthly Milestone";
            else if (daysSinceAwarded <= 90)
                return "Quarterly Achievement";
            else if (daysSinceAwarded <= 365)
                return "Annual Milestone";
            else
                return "Legacy Achievement";
        }

        public string GetCelebrationMessage()
        {
            var category = GetAchievementCategory();
            return category switch
            {
                "Recent Achievement" => "🎉 Fresh achievement! Keep up the great work!",
                "Monthly Milestone" => "💪 Solid progress over the month. Well done!",
                "Quarterly Achievement" => "🏆 Excellent commitment over the quarter!",
                "Annual Milestone" => "🌟 Outstanding dedication over the year!",
                "Legacy Achievement" => "👑 Legendary achievement! A true inspiration!",
                _ => "🎖️ Remarkable health achievement!"
            };
        }

        public string GetUserDisplayName()
        {
            if (Item.User != null)
                return Item.User.Email ?? "Unknown User";
            return "Unknown User";
        }

        public string GetBadgeDisplayInfo()
        {
            if (Item.Badge != null)
                return $"{Item.Badge.Name} ({Item.Badge.Code})";
            return "Unknown Badge";
        }

        public string GetBadgeCriteria()
        {
            if (Item.Badge != null && !string.IsNullOrEmpty(Item.Badge.Criteria))
                return Item.Badge.Criteria;
            return "No criteria specified";
        }

        public string GetBadgeDescription()
        {
            if (Item.Badge != null && !string.IsNullOrEmpty(Item.Badge.Description))
                return Item.Badge.Description;
            return "No description available";
        }

        public bool IsRecentAward()
        {
            return (DateTime.Now - Item.AwardedAt).TotalDays <= 30;
        }

        public string GetAwardSeasonality()
        {
            var month = Item.AwardedAt.Month;
            return month switch
            {
                12 or 1 or 2 => "Winter Achievement",
                3 or 4 or 5 => "Spring Achievement",
                6 or 7 or 8 => "Summer Achievement",
                9 or 10 or 11 => "Fall Achievement",
                _ => "Seasonal Achievement"
            };
        }
    }
}