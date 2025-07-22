using BusinessLogic;
using BusinessLogic.IService;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Presentation.Pages.QuitPlanPage
{
    public class IndexModel : PageModel
    {

        private readonly IQuitPlanService _quitPlanService;

        public IndexModel(IQuitPlanService quitPlanService)
        {
            _quitPlanService = quitPlanService;
        }

        [BindProperty]
        public List<QuitPlan> QuitPlans { get; set; }

        [BindProperty]
        public PlanInputModel Plan { get; set; }
        public void OnGet()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            QuitPlans = _quitPlanService.GetAllByUserId(user.UserId);
        }
        [BindProperty]
        public int PlanId { get; set; }

        [BindProperty]
        public QuitPlan NewPlan { get; set; }
        public IActionResult OnPostEdit()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            if (!ModelState.IsValid)
            {
                QuitPlans = _quitPlanService.GetAllByUserId(user.UserId);
                return Page();
            }

            QuitPlan existingPlan = _quitPlanService.GetById(Plan.PlanId);

            existingPlan.PlanId = Plan.PlanId;
            existingPlan.Reason = Plan.Reason;
            existingPlan.PlanDetails = Plan.DetailedPlan;
            existingPlan.StartDate = DateOnly.FromDateTime(Plan.StartDate);
            existingPlan.ExpectedEndDate = DateOnly.FromDateTime(Plan.ExpectedEndDate);
            existingPlan.Status = Plan.Status;

            _quitPlanService.Update(existingPlan);

            TempData["SuccessMessage"] = "Kế hoạch đã được sửa!";
            return RedirectToPage();
        }

        public IActionResult OnPostCreate()
        {
            User user = HttpContext.Session.GetObject<User>("user");

            NewPlan.UserId = user.UserId;

            _quitPlanService.Create(NewPlan);

            TempData["SuccessMessage"] = "Kế hoạch đã được tao!";
            return RedirectToPage();
        }
        public IActionResult OnPostDelete()
        {

            QuitPlan existingPlan = _quitPlanService.GetById(Plan.PlanId);

            existingPlan.IsDeleted = true;

            _quitPlanService.Delete(existingPlan);

            TempData["SuccessMessage"] = "Kế hoạch đã được tao!";
            return RedirectToPage();
        }

        public class PlanInputModel
        {
            public int PlanId { get; set; }

            [Required]
            public string Reason { get; set; }

            public string DetailedPlan { get; set; }

            [DataType(DataType.Date)]
            public DateTime StartDate { get; set; }

            [DataType(DataType.Date)]
            public DateTime ExpectedEndDate { get; set; }

            [Required]
            public string Status { get; set; }
        }
    }
}
