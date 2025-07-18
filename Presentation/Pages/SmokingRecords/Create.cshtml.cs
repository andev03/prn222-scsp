using BusinessLogic.IServices;
using BusinessObject;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Pages.SmokingRecords
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public SmokingRecord Item { get; set; }

        private readonly ISmokingRecordService _smokingRecordService;

        public CreateModel(ISmokingRecordService smokingRecordService)
        {
            _smokingRecordService = smokingRecordService;
        }

        public async Task OnGetAsync()
        {

            Item = new SmokingRecord
            {
                RecordDate = DateOnly.FromDateTime(DateTime.Now),
                CigarettesCount = 0,
                Disabled = false,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            Item.UserId = user.UserId;

            // Validate required fields manually
            if (Item.RecordDate == default(DateOnly))
            {
                ModelState.AddModelError("Item.RecordDate", "Record Date is required.");
            }

            if (Item.UserId == Guid.Empty)
            {
                ModelState.AddModelError("Item.UserId", "Please select a user.");
            }

            if (Item.CigarettesCount < 0)
            {
                ModelState.AddModelError("Item.CigarettesCount", "Cigarettes count cannot be negative.");
            }

            if (Item.CostPerPack.HasValue && Item.CostPerPack < 0)
            {
                ModelState.AddModelError("Item.CostPerPack", "Cost per pack cannot be negative.");
            }

            // Check if record date is in the future
            if (Item.RecordDate > DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("Item.RecordDate", "Record date cannot be in the future.");
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

                if (!Item.Disabled.HasValue)
                {
                    Item.Disabled = false;
                }

                await _smokingRecordService.Create(Item);

                TempData["SuccessMessage"] = $"Smoking record for '{Item.RecordDate.ToString("MMM dd, yyyy")}' created successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                ModelState.AddModelError("", $"Error creating smoking record: {ex.Message}");
                return Page();
            }
        }
    }
}