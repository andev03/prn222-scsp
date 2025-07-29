using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalPackages { get; set; }
        public int TotalSubscriptions { get; set; }
        public int TotalQuitPlans { get; set; }
        public int TotalFeedbacks { get; set; }
        public double AverageRating { get; set; }
        public int TotalBadgesAwarded { get; set; }
        public int TotalCoachSessions { get; set; }
        public int TotalForumPosts { get; set; }
        public decimal TotalPayments { get; set; }
    }
}
