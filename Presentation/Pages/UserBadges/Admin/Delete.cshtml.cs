using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IServices;

namespace Presentation.Pages.UserBadges.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IUserBadgeService _userBadgeService;

        public DeleteModel(IUserBadgeService userBadgeService)
        {
            _userBadgeService = userBadgeService;
        }

        [BindProperty]
        public UserBadge UserBadge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userbadge = await _userBadgeService.GetByIdAsync(id);

            if (userbadge == null)
            {
                return NotFound();
            }
            else
            {
                UserBadge = userbadge;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

       
// Xóa thiệt
              await   _userBadgeService.DeleteBadgeAsync(id);
          

            return RedirectToPage("./Index");
        }
    }
}
