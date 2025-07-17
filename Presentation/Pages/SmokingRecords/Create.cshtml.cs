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
        public SmokingRecord Item { get; set; } = new SmokingRecord();
        public List<SelectListItem> UserList { get; set; } = new List<SelectListItem>();

        private readonly ISmokingRecordService _smokingRecordService;
        private readonly IUserService _userService;

        public CreateModel(ISmokingRecordService smokingRecordService, IUserService userService)
        {
            _smokingRecordService = smokingRecordService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            // Load users for dropdown
            await LoadUsers();

            // Initialize default values
            Item = new SmokingRecord
            {
                RecordDate = DateOnly.FromDateTime(DateTime.Now),
                CigarettesCount = 0,
                Disabled = false,
                CreatedAt = DateTime.Now,
                IsDeleted = false
                // UserId will be selected from dropdown
            };
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

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload users for dropdown in case of validation errors
            await LoadUsers();

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

                // Ensure Disabled has a value (default to false if null)
                if (!Item.Disabled.HasValue)
                {
                    Item.Disabled = false;
                }

                // Create smoking record through service
                await _smokingRecordService.Create(Item);

                // Redirect back to index with success message
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