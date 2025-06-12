using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models.Entities;
using SCSP.DataAccess.DatabaseContexts;

namespace SCSP.RazorApp
{
    public class DetailsModel : PageModel
    {
        private readonly QuitSmokingAppDbContext _context;

        public DetailsModel(QuitSmokingAppDbContext context)
        {
            _context = context;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Guid == id);

            if (user is not null)
            {
                User = user;

                return Page();
            }

            return NotFound();
        }
    }
}
