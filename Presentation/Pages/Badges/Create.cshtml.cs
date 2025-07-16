using BusinessLogic.IServices;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Badges
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        public Badge Item { get; set; } = new Badge();

        private readonly IBadgeService _badgeService;

        public CreateModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        public void OnGet()
        {
            // Initialize default values
            Item = new Badge
            {
                Disabled = false, // Default to active (not disabled)
                CreatedAt = DateTime.Now,
                IsDeleted = false
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate required fields manually
            if (string.IsNullOrWhiteSpace(Item.Code))
            {
                ModelState.AddModelError("Item.Code", "Badge Code is required.");
            }

            if (string.IsNullOrWhiteSpace(Item.Name))
            {
                ModelState.AddModelError("Item.Name", "Badge Name is required.");
            }

            if (string.IsNullOrWhiteSpace(Item.Description))
            {
                ModelState.AddModelError("Item.Description", "Description is required.");
            }

            if (string.IsNullOrWhiteSpace(Item.Criteria))
            {
                ModelState.AddModelError("Item.Criteria", "Achievement Criteria is required.");
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
                //Item.Code = "das";
                // Ensure Disabled has a value (default to false if null)
                if (!Item.Disabled.HasValue)
                {
                    Item.Disabled = false;
                }

                // Create badge through service
                await _badgeService.Create(Item);

                // Redirect back to index with success message
                TempData["SuccessMessage"] = $"Badge '{Item.Name}' created successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                ModelState.AddModelError("", $"Error creating badge: {ex.Message}");
                return Page();
            }
        }
    }
}