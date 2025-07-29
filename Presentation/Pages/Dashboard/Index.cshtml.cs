using BusinessLogic;
using BusinessObject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Dashboard
{
    public class IndexModel : PageModel
    {
        private readonly DashboardService _dashboardService;

        public DashboardViewModel Dashboard { get; set; } = new();

        public IndexModel(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task OnGetAsync()
        {
            Dashboard = await _dashboardService.GetDashboardDataAsync();
        }
    }
}
