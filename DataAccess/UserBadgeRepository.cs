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
            var check = await _context.UserBadges.Include(x=>x.User).Include(x=>x.Badge).ToListAsync();
            return check;
        }
        public async Task<UserBadge> GetByIdDetails(int id)
        {
            var check = await _context.UserBadges.Include(x => x.User).Include(x => x.Badge).Where(x=>x.UserBadgeId== id).FirstOrDefaultAsync();
            return check;
        }
    }
}
