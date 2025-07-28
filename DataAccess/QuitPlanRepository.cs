using BusinessObject.Models;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
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

        //Admin 
        public async Task<List<QuitPlan>> GetAllQuitPlanAsync()
        {
            return await _context.QuitPlans
                                 .Include(q => q.User) // Đảm bảo có thông tin User
                                 .ToListAsync();
        }


        public async Task<List<QuitPlan>> GetQuitPlansByUserIdAsync(Guid userId)
      => await _context.QuitPlans
            .Include(p => p.User)
          .Include(p => p.ProgressRecords)
          .Where(p => p.UserId == userId)
          .ToListAsync();

        public async Task<QuitPlan> GetByIdAsync(int id)
            => await _context.QuitPlans.Include(u => u.User)
                .Include(p => p.ProgressRecords)
                .FirstOrDefaultAsync(p => p.PlanId == id);

        public async Task AddAsync(QuitPlan plan)
        {
            _context.QuitPlans.Add(plan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(QuitPlan plan)
        {
            _context.QuitPlans.Update(plan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plan = await GetByIdAsync(id);
            if (plan != null)
            {
                _context.QuitPlans.Remove(plan);
                await _context.SaveChangesAsync();
            }
        }

    }
}
