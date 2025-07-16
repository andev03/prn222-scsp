using BusinessLogic.IServices;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.SmokingRecords
{
    public class DetailModel : PageModel
    {
        public SmokingRecord Item { get; set; } = new SmokingRecord();
        private readonly ISmokingRecordService _smokingRecordService;

        public DetailModel(ISmokingRecordService smokingRecordService)
        {
            _smokingRecordService = smokingRecordService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                // Get smoking record by ID
                Item = await _smokingRecordService.GetById(id);

                if (Item == null || Item.IsDeleted == true)
                {
                    TempData["ErrorMessage"] = "Smoking record not found or has been deleted.";
                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading smoking record: {ex.Message}";
                return RedirectToPage("./Index");
            }
        }

        // Helper methods for health analysis
        public string GetHealthStatus()
        {
            if (Item.CigarettesCount == 0)
                return "Excellent";
            else if (Item.CigarettesCount <= 3)
                return "Good Progress";
            else if (Item.CigarettesCount <= 10)
                return "Moderate";
            else
                return "High Risk";
        }

        public string GetHealthStatusColor()
        {
            var status = GetHealthStatus();
            return status switch
            {
                "Excellent" => "success",
                "Good Progress" => "info",
                "Moderate" => "warning",
                "High Risk" => "danger",
                _ => "secondary"
            };
        }

        public string GetHealthStatusIcon()
        {
            var status = GetHealthStatus();
            return status switch
            {
                "Excellent" => "fas fa-heart",
                "Good Progress" => "fas fa-heart-pulse",
                "Moderate" => "fas fa-exclamation-triangle",
                "High Risk" => "fas fa-radiation",
                _ => "fas fa-question"
            };
        }

        public string GetFrequencyDisplayText()
        {
            if (string.IsNullOrEmpty(Item.Frequency))
                return "Not specified";

            return Item.Frequency;
        }

        public decimal GetEstimatedDailyCost()
        {
            if (!Item.CostPerPack.HasValue || Item.CigarettesCount == 0)
                return 0;

            // Assuming 20 cigarettes per pack
            const int cigarettesPerPack = 20;
            return (Item.CostPerPack.Value / cigarettesPerPack) * Item.CigarettesCount;
        }

        public string GetMotivationalMessage()
        {
            if (Item.CigarettesCount == 0)
                return "🎉 Congratulations on a smoke-free day! Your lungs are thanking you.";
            else if (Item.CigarettesCount <= 3)
                return "💪 Great progress! You're reducing your consumption significantly.";
            else if (Item.CigarettesCount <= 10)
                return "🔄 Keep working towards fewer cigarettes. Every reduction counts!";
            else
                return "💭 Consider strategies to reduce consumption. Your health journey matters.";
        }
    }
}