using BusinessObject.Models;
using DataAccess.Base;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserBadgeRepository : GenericRepository<UserBadge>, IUserBadgeRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public UserBadgeRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public List<UserBadge> GetAll()
        {
            return _context.UserBadges.Include(x => x.User).ToList();
        }

        public async Task<List<UserBadge>> GetAllDetails()
        {
            var check = await _context.UserBadges.Include(x => x.User).Include(x => x.Badge).ToListAsync();
            return check;
        }
        public async Task<UserBadge> GetByIdDetails(int id)
        {
            var check = await _context.UserBadges.Include(x => x.User).Include(x => x.Badge).Where(x => x.UserBadgeId == id).FirstOrDefaultAsync();
            return check;
        }


        //Admin
        public async Task<List<UserBadge>> GetAllAsync()
        {
            return await _context.UserBadges.Include(ub => ub.User)
                                            .Include(ub => ub.Badge)
                                            .ToListAsync();
        }

        public async Task<UserBadge> GetByIdAsync(int id)
        {
            return await _context.UserBadges.Include(ub => ub.User)
                                            .Include(ub => ub.Badge)
                                            .FirstOrDefaultAsync(ub => ub.UserBadgeId == id);
        }

        public async Task<List<UserBadge>> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserBadges.Include(ub => ub.Badge)
                                            .Where(ub => ub.UserId == userId)
                                            .ToListAsync();
        }

        public async Task AddBadgeAsync(UserBadge userBadge)
        {
            _context.UserBadges.Add(userBadge);
            await _context.SaveChangesAsync();
        }

        

        // Xóa thiệt
        public async Task DeleteBadgeAsync(int id)
        {
            var entity = await _context.UserBadges.FindAsync(id);
            if (entity != null)
            {
                _context.UserBadges.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
