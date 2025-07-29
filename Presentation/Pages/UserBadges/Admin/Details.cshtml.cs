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
    public class DetailsModel : PageModel
    {
        private readonly IUserBadgeService _userBadgeService;

        public DetailsModel(IUserBadgeService userBadgeService)
        {
            _userBadgeService = userBadgeService;
        }

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
    }
}
