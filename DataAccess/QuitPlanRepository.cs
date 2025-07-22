using BusinessObject.Models;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class QuitPlanRepository : IQuitPlanRepository
    {

        private readonly QuitSmokingAppDBContext _context;
        public QuitPlanRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public void Create(QuitPlan quitPlan)
        {
            _context.QuitPlans.Add(quitPlan);
            _context.SaveChanges();
        }

        public List<QuitPlan> GetAllByUserId(Guid userId)
        {
            return _context.QuitPlans
                .Where(q => q.UserId == userId && q.IsDeleted == false)
                .OrderByDescending(q => q.CreatedAt)
                .ToList();
        }

        public QuitPlan GetById(int planId)
        {
            return _context.QuitPlans.FirstOrDefault(qp => qp.PlanId == planId);
        }

        public void Update(QuitPlan quitPlan)
        {
            _context.QuitPlans.Update(quitPlan);
            _context.SaveChanges();
        }

        public void Delete(QuitPlan quitPlan)
        {
            _context.QuitPlans.Update(quitPlan);
            _context.SaveChanges();
        }
    }
}
