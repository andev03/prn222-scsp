using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models.Entities;
using SCSP.DataAccess.DatabaseContexts;

namespace SCSP.RazorApp
{
    public class IndexModel : PageModel
    {
        private readonly QuitSmokingAppDbContext _context;

        public IndexModel(QuitSmokingAppDbContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}
