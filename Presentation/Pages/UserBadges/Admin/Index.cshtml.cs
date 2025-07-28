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

namespace Presentation.Pages.UserBadges.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IUserBadgeService _userBadgeService;

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int pageNumber { get; set; }


        public IndexModel(IUserBadgeService userBadgeService)
        {
            _userBadgeService = userBadgeService;
        }

        public IList<UserBadge> UserBadge { get;set; } = default!;

        public async Task OnGetAsync(int? pageNumber)
        {
            int pageSize = 10;
            CurrentPage = pageNumber ?? 1;

            var allBadges = await _userBadgeService.GetAllAsync();
            var totalCount = allBadges.Count();

            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            UserBadge = allBadges
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

    }
}
