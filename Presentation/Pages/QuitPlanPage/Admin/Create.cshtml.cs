using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Models;
using BusinessLogic.IService;
using BusinessLogic.IServices;

namespace Presentation.Pages.QuitPlanPage.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IQuitPlanService _quitPlanService;
        private readonly IUserService _userService;

        public CreateModel(IQuitPlanService quitPlanService, IUserService userService)
        {
            _quitPlanService = quitPlanService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGet()
        {
            ViewData["UserId"] = new SelectList(await _userService.GetAllUsers(), "UserId", "Email");
            return Page();
        }

        [BindProperty]
        public QuitPlan QuitPlan { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _quitPlanService.AddAsync(QuitPlan);

            return RedirectToPage("./Index");
        }
    }
}
