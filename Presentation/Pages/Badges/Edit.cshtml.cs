using BusinessLogic.IServices;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Badges
{
    [BindProperties]
    public class EditModel : PageModel
    {
        public Badge Item { get; set; } = new Badge();
        private readonly IBadgeService _badgeService;

        public EditModel(IBadgeService badgeService)
        {
            _badgeService = badgeService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                // Get badge by ID
                Item = await _badgeService.GetById(id);

                if (Item == null)
                {
                    TempData["ErrorMessage"] = "Badge not found ";
                    return RedirectToPage("./Index");
                }

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error loading badge: {ex.Message}";
                return RedirectToPage("./Index");
            }
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
                Item.UpdatedAt = DateTime.Now;

                // Ensure Disabled has a value (default to false if null)
                if (!Item.Disabled.HasValue)
                {
                    Item.Disabled = false;
                }

                // Update badge through service
                await _badgeService.Update(Item);

                // Redirect back to index with success message
                TempData["SuccessMessage"] = $"Badge '{Item.Name}' updated successfully!";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Handle errors
                ModelState.AddModelError("", $"Error updating badge: {ex.Message}");
                return Page();
            }
        }
    }
}