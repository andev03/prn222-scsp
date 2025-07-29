using BusinessObject.ViewModels;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
  public  class DashboardService
    {
        private readonly DashboardRepository _context;

        public DashboardService()
        {
            _context = new DashboardRepository();
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {

            return await _context.GetDashboardDataAsync();
        }
    }
}
