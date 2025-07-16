using BusinessObject;
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
    public class BadgeRepository : GenericRepository<Badge>, IBadgeRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public BadgeRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

     

        public async Task<bool> IsExist(int id)
        {
            var item = await _context.Badges.FirstOrDefaultAsync(x => x.Code == id + "");
            return (item==null)?false:true;
        }
    }
}
