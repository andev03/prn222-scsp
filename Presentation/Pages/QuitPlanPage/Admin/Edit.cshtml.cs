using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IService;
using BusinessLogic.IServices;

namespace Presentation.Pages.QuitPlanPage.Admin
{
    public class EditModel : PageModel
    {
        private readonly IQuitPlanService _quitPlanService;
        private readonly IUserService _userService;

        public EditModel(IQuitPlanService quitPlanService, IUserService userService)
        {
            _quitPlanService = quitPlanService;
            _userService = userService;
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
            QuitPlan = quitplan;
            ViewData["UserId"] = new SelectList(await _userService.GetAllUsers(), "UserId", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _quitPlanService.UpdateAsync(QuitPlan);
            }
            catch (DbUpdateConcurrencyException)
            {

                throw;

            }

            return RedirectToPage("./Index");
        }

        //private bool QuitPlanExists(int id)
        //{
        //    return _context.QuitPlans.Any(e => e.PlanId == id);
        //}
    }
}
