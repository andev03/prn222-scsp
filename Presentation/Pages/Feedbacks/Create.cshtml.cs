using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;

namespace Presentation.Pages.Feedbacks
{
    public class CreateModel : PageModel
    {
        private readonly BusinessObject.Models.QuitSmokingAppDBContext _context;

        public CreateModel(BusinessObject.Models.QuitSmokingAppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CoachId"] = new SelectList(_context.Users, "UserId", "Email");
        ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Feedbacks.Add(Feedback);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
