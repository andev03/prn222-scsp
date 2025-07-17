using BusinessLogic.IServices;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.SmokingRecords
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ISmokingRecordService _smokingRecordService;
        public List<SmokingRecord> smokingRecords { get; set; } = new List<SmokingRecord>();

        public IndexModel(ISmokingRecordService smokingRecordService)
        {
            _smokingRecordService = smokingRecordService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                smokingRecords = await _smokingRecordService.GetAll();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading smoking records: {ex.Message}";
                smokingRecords = new List<SmokingRecord>();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                // Get record to check if it exists
                var record = await _smokingRecordService.GetById(id);
                if (record == null)
                {
                    TempData["ErrorMessage"] = "Smoking record not found.";
                    return RedirectToPage();
                }

                // Store record date for success message
                string recordDate = record.RecordDate.ToString("MMM dd, yyyy");

                // Delete record through service
                await _smokingRecordService.SoftDelete(id);

                TempData["SuccessMessage"] = $"Smoking record for '{recordDate}' has been deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting smoking record: {ex.Message}";
            }

            return RedirectToPage();
        }

        // Health statistics methods for better organization
        public int GetTotalRecords()
        {
            return smokingRecords?.Count ?? 0;
        }

        public int GetSmokeFreeDays()
        {
            return smokingRecords?.Count(r => r.CigarettesCount == 0 && r.IsDeleted != true) ?? 0;
        }

        public int GetTotalCigarettes()
        {
            return smokingRecords?.Where(r => r.IsDeleted != true).Sum(r => r.CigarettesCount) ?? 0;
        }

        public decimal GetTotalCost()
        {
            return smokingRecords?.Where(r => r.CostPerPack.HasValue && r.IsDeleted != true)
                                  .Sum(r => r.CostPerPack.Value) ?? 0;
        }

        public double GetHealthProgressPercentage()
        {
            var activeRecords = smokingRecords?.Where(r => r.IsDeleted != true).ToList();
            if (activeRecords == null || !activeRecords.Any())
                return 0;

            var smokeFreeCount = activeRecords.Count(r => r.CigarettesCount == 0);
            return (double)smokeFreeCount / activeRecords.Count * 100;
        }

        public string GetHealthStatus()
        {
            var progressPercentage = GetHealthProgressPercentage();

            if (progressPercentage >= 80)
                return "Excellent";
            else if (progressPercentage >= 60)
                return "Good";
            else if (progressPercentage >= 40)
                return "Improving";
            else if (progressPercentage >= 20)
                return "Moderate";
            else
                return "Needs Attention";
        }

        public string GetHealthStatusColor()
        {
            var status = GetHealthStatus();
            return status switch
            {
                "Excellent" => "success",
                "Good" => "info",
                "Improving" => "warning",
                "Moderate" => "warning",
                "Needs Attention" => "danger",
                _ => "secondary"
            };
        }

        // Get recent smoking trend (last 7 days)
        public string GetRecentTrend()
        {
            var recentRecords = smokingRecords?
                .Where(r => r.IsDeleted != true && r.RecordDate >= DateOnly.FromDateTime(DateTime.Now.AddDays(-7)))
                .OrderBy(r => r.RecordDate)
                .ToList();

            if (recentRecords == null || recentRecords.Count < 2)
                return "Insufficient data";

            var firstHalf = recentRecords.Take(recentRecords.Count / 2).Average(r => r.CigarettesCount);
            var secondHalf = recentRecords.Skip(recentRecords.Count / 2).Average(r => r.CigarettesCount);

            if (secondHalf < firstHalf)
                return "Improving";
            else if (secondHalf > firstHalf)
                return "Declining";
            else
                return "Stable";
        }

        // Get average daily cigarettes
        public double GetAverageDailyCigarettes()
        {
            var activeRecords = smokingRecords?.Where(r => r.IsDeleted != true).ToList();
            if (activeRecords == null || !activeRecords.Any())
                return 0;

            return activeRecords.Average(r => r.CigarettesCount);
        }
    }
}