using BusinessObject.Models;
using BusinessObject.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public  class DashboardRepository
    {

        private readonly QuitSmokingAppDBContext _context;

        public DashboardRepository( )
        {
            _context = new QuitSmokingAppDBContext();
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync()
        {
            return new DashboardViewModel
            {
                TotalUsers = await _context.Users.CountAsync(),
         
                TotalSubscriptions = await _context.Subscriptions.CountAsync(),
                TotalQuitPlans = await _context.QuitPlans.CountAsync(),
                TotalFeedbacks = await _context.Feedbacks.CountAsync(),
                AverageRating = await _context.Feedbacks.AnyAsync()
                    ? await _context.Feedbacks.AverageAsync(f => f.Rating)
                    : 0,
                TotalBadgesAwarded = await _context.UserBadges.CountAsync(),
                TotalForumPosts = await _context.ForumPosts.CountAsync(),
                TotalPayments = await _context.Payments.SumAsync(p => p.Amount)
            };
        }

    }
}
