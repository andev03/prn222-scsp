using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IServices;

namespace Presentation.Pages.Feedbacks
{
    public class DeleteModel : PageModel
    {
        private readonly IFeedbackService _context;

        public DeleteModel(IFeedbackService context)
        {
            _context = context;
        }


        [BindProperty]
        public Feedback Feedback { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.GetByIdAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }
            else
            {
                Feedback = feedback;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feedback = await _context.GetByIdAsync(id);
            if (feedback != null)
            {
                Feedback = feedback;
                await _context.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
