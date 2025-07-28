using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IServices;
using BusinessLogic;

namespace Presentation.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;

        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
