using BusinessLogic.IServices;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.UserBadges
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly IUserBadgeService _userBadgeService;
        public List<UserBadge> userBadges { get; set; } = new List<UserBadge>();

        public IndexModel(IUserBadgeService userBadgeService)
        {
            _userBadgeService = userBadgeService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                userBadges = await _userBadgeService.GetAll();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading user badge awards: {ex.Message}";
                userBadges = new List<UserBadge>();
            }
        }

        public async Task<IActionResult> OnPostRevokeAsync(int id)
        {
            try
            {
                // Get user badge to check if it exists
                var userBadge = await _userBadgeService.GetById(id);
                if (userBadge == null)
                {
                    TempData["ErrorMessage"] = "Badge award not found.";
                    return RedirectToPage();
                }

                // Store user and badge info for success message
                string userEmail = userBadge.User?.Email ?? "Unknown User";
                string badgeName = userBadge.Badge?.Name ?? "Unknown Badge";

                // Delete/revoke user badge through service
                await _userBadgeService.SoftDelete(id);

                TempData["SuccessMessage"] = $"Badge '{badgeName}' has been revoked from user '{userEmail}' successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error revoking badge award: {ex.Message}";
            }

            return RedirectToPage();
        }

        // Achievement statistics methods
        public int GetTotalAwards()
        {
            return userBadges?.Count ?? 0;
        }

        public int GetActiveAwards()
        {
            return userBadges?.Count(ub => ub.Disabled != true && ub.IsDeleted != true) ?? 0;
        }

        public int GetUniqueUsers()
        {
            return userBadges?.Where(ub => ub.IsDeleted != true)
                             .Select(ub => ub.UserId)
                             .Distinct()
                             .Count() ?? 0;
        }

        public int GetTodaysAwards()
        {
            return userBadges?.Where(ub => ub.AwardedAt.Date == DateTime.Today && ub.IsDeleted != true)
                             .Count() ?? 0;
        }

        public int GetDisabledAwards()
        {
            return userBadges?.Count(ub => ub.Disabled == true && ub.IsDeleted != true) ?? 0;
        }

        // Achievement analysis methods
        public int GetRecentAwards(int days = 7)
        {
            var cutoffDate = DateTime.Now.AddDays(-days);
            return userBadges?.Where(ub => ub.AwardedAt >= cutoffDate && ub.IsDeleted != true)
                             .Count() ?? 0;
        }

        public List<UserBadge> GetTopRecentAwards(int count = 5)
        {
            return userBadges?.Where(ub => ub.IsDeleted != true)
                             .OrderByDescending(ub => ub.AwardedAt)
                             .Take(count)
                             .ToList() ?? new List<UserBadge>();
        }

        public Dictionary<string, int> GetBadgeDistribution()
        {
            var distribution = new Dictionary<string, int>();

            if (userBadges == null) return distribution;

            var activeAwards = userBadges.Where(ub => ub.IsDeleted != true && ub.Badge != null);

            foreach (var award in activeAwards)
            {
                var badgeName = award.Badge.Name ?? "Unknown";
                if (distribution.ContainsKey(badgeName))
                    distribution[badgeName]++;
                else
                    distribution[badgeName] = 1;
            }

            return distribution.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public Dictionary<string, int> GetMonthlyAwardTrend()
        {
            var monthlyTrend = new Dictionary<string, int>();

            if (userBadges == null) return monthlyTrend;

            var last6Months = userBadges.Where(ub => ub.IsDeleted != true &&
                                                    ub.AwardedAt >= DateTime.Now.AddMonths(-6))
                                        .GroupBy(ub => ub.AwardedAt.ToString("yyyy-MM"))
                                        .ToDictionary(g => g.Key, g => g.Count());

            // Fill in missing months with 0
            for (int i = 5; i >= 0; i--)
            {
                var month = DateTime.Now.AddMonths(-i).ToString("yyyy-MM");
                monthlyTrend[month] = last6Months.ContainsKey(month) ? last6Months[month] : 0;
            }

            return monthlyTrend;
        }

        // User engagement metrics
        public List<UserAchievementSummary> GetTopAchievers(int count = 10)
        {
            if (userBadges == null) return new List<UserAchievementSummary>();

            return userBadges.Where(ub => ub.IsDeleted != true && ub.User != null)
                            .GroupBy(ub => new { ub.UserId, ub.User.Email })
                            .Select(g => new UserAchievementSummary
                            {
                                UserId = g.Key.UserId,
                                UserEmail = g.Key.Email,
                                TotalBadges = g.Count(),
                                ActiveBadges = g.Count(ub => ub.Disabled != true),
                                LatestAward = g.Max(ub => ub.AwardedAt)
                            })
                            .OrderByDescending(s => s.TotalBadges)
                            .Take(count)
                            .ToList();
        }

        public double GetAverageAwardsPerUser()
        {
            var uniqueUsers = GetUniqueUsers();
            if (uniqueUsers == 0) return 0;

            return (double)GetActiveAwards() / uniqueUsers;
        }

        // Health progress insights
        public string GetAchievementTrend()
        {
            var last30Days = userBadges?.Where(ub => ub.AwardedAt >= DateTime.Now.AddDays(-30) && ub.IsDeleted != true).Count() ?? 0;
            var previous30Days = userBadges?.Where(ub => ub.AwardedAt >= DateTime.Now.AddDays(-60) &&
                                                       ub.AwardedAt < DateTime.Now.AddDays(-30) &&
                                                       ub.IsDeleted != true).Count() ?? 0;

            if (previous30Days == 0) return "New";

            var percentageChange = ((double)(last30Days - previous30Days) / previous30Days) * 100;

            if (percentageChange > 10) return "Growing";
            else if (percentageChange < -10) return "Declining";
            else return "Stable";
        }

        public string GetAchievementTrendColor()
        {
            var trend = GetAchievementTrend();
            return trend switch
            {
                "Growing" => "success",
                "Declining" => "danger",
                "Stable" => "info",
                "New" => "primary",
                _ => "secondary"
            };
        }
    }

    // Helper class for user achievement summary
    public class UserAchievementSummary
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public int TotalBadges { get; set; }
        public int ActiveBadges { get; set; }
        public DateTime LatestAward { get; set; }
    }
}