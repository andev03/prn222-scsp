using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;

namespace Presentation.Pages.UserBadges.Admin
{
    public class EditModel : PageModel
    {
        private readonly BusinessObject.Models.QuitSmokingAppDBContext _context;

        public EditModel(BusinessObject.Models.QuitSmokingAppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserBadge UserBadge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userbadge =  await _context.UserBadges.FirstOrDefaultAsync(m => m.UserBadgeId == id);
            if (userbadge == null)
            {
                return NotFound();
            }
            UserBadge = userbadge;
           ViewData["BadgeId"] = new SelectList(_context.Badges, "BadgeId", "Code");
           ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserBadge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserBadgeExists(UserBadge.UserBadgeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserBadgeExists(int id)
        {
            return _context.UserBadges.Any(e => e.UserBadgeId == id);
        }
    }
}
