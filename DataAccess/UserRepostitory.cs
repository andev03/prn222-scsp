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
    public class UserRepostitory :GenericRepository<User>, IUserRepostitory
    {
        private readonly QuitSmokingAppDBContext _quitSmokingAppDBContext;
        public UserRepostitory()
        {
            _context = new QuitSmokingAppDBContext();
        }
        public async Task<User> GetByGuiUser(Guid id) {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

    }
}
