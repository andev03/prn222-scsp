using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IService;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLogic.IServices;

namespace Presentation.Pages.QuitPlanPage.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IQuitPlanService _quitPlanService;
        private readonly IUserService _userService;
 

        public IndexModel(IQuitPlanService quitPlanService, IUserService userService)
        {
            _quitPlanService = quitPlanService;
            _userService = userService;
        }

        public IList<QuitPlan> QuitPlan { get;set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Page { get; set; } = 1;

      
        public int? UserId { get; set; }
        public async Task OnGetAsync(Guid? userId, int pageNumber = 1)
        {
            int pageSize = 10;

            var result = await _quitPlanService.GetAllQuitPlanAsync();

            var filteredUsers = result
                .Where(q => q.User != null)
                .Select(q => q.User)
                .DistinctBy(u => u.UserId)
                .ToList();

            ViewData["UserId"] = new SelectList(filteredUsers, "UserId", "Email", userId);

            var filteredPlans = userId.HasValue
                ? await _quitPlanService.GetQuitPlansByUserIdAsync(userId.Value)
                : result;

            // Phân trang dữ liệu
            QuitPlan = filteredPlans
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // Tính tổng số trang
            TotalPages = (int)Math.Ceiling(filteredPlans.Count / (double)pageSize);
            CurrentPage = pageNumber;
        }



    }
}
