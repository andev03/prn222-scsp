using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using BusinessLogic.IServices;

namespace Presentation.Pages.UserBadges.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IUserBadgeService _userBadgeService;
        private readonly IUserService _userService;

        private readonly IBadgeService _badgeService;
        public CreateModel(IUserBadgeService userBadgeService, IUserService userService, IBadgeService badgeService)
        {
            _userBadgeService = userBadgeService;
            _userService = userService;
            _badgeService = badgeService;
        }

        public async Task<IActionResult> OnGet()
        {
            var badges = await _badgeService.GetAll() ?? new List<Badge>();
            var users = await _userService.GetAllUsers() ?? new List<User>();

            ViewData["BadgeId"] = new MultiSelectList(badges, "BadgeId", "Code");
            ViewData["UserId"] = new SelectList(users, "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public UserBadge UserBadge { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userBadgeService.AddBadgeAsync(UserBadge);

            return RedirectToPage("./Index");
        }
    }
}
