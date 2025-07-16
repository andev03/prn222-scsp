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
            _quitSmokingAppDBContext = new QuitSmokingAppDBContext();
        }
        public async Task<User> GetByGuiUser(Guid id) {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public User Login(string username, string password)
        {
            return _quitSmokingAppDBContext.Users.FirstOrDefault(u => u.Username.Equals(username) && u.PasswordHash.Equals(password));
        }
    }
}
