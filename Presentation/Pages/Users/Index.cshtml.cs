using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IServices;

namespace Presentation.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public IList<User> User { get;set; } = default!;

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public async Task<IActionResult> OnGetAsync(int page = 1)
        {
            int pageSize = 10;
            var users = await _userService.GetAllUsers();

            TotalPages = (int)Math.Ceiling(users.Count / (double)pageSize);
            CurrentPage = page;

            User = users
                .OrderByDescending(u => u.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Page();
        }
    }
}
