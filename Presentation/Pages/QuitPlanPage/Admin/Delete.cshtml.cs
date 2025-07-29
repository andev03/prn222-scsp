using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IService;

namespace Presentation.Pages.QuitPlanPage.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IQuitPlanService _quitPlanService;

        public DeleteModel(IQuitPlanService quitPlanService)
        {
            _quitPlanService = quitPlanService;
        }

        [BindProperty]
        public QuitPlan QuitPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quitplan = await _quitPlanService.GetByIdAsync(id);

            if (quitplan == null)
            {
                return NotFound();
            }
            else
            {
                QuitPlan = quitplan;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quitplan = await _quitPlanService.GetByIdAsync(id);
            if (quitplan != null)
            {
                QuitPlan = quitplan;
                await _quitPlanService.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
