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
    public class UserRepostitory :GenericRepository<User>, IUserRepostitory
    {
        private readonly QuitSmokingAppDBContext _quitSmokingAppDBContext;
        public UserRepostitory()
        {
            _quitSmokingAppDBContext = new QuitSmokingAppDBContext();
        }

        public List<User> GetAllByRole()
        {
            return _quitSmokingAppDBContext.Users.Where(u => u.Role.ToLower() == "coach").ToList();
        }

        public async Task<User> GetByGuiUser(Guid id) {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public User Login(string username, string password)
        {
            return _quitSmokingAppDBContext.Users.FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
        }

        public void Register(User user)
        {
            _quitSmokingAppDBContext.Add(user);
            _quitSmokingAppDBContext.SaveChanges();
        }

        public void UpdateProfile(User user)
        {
            var existingUser = _quitSmokingAppDBContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            _quitSmokingAppDBContext.Update(existingUser);
            _quitSmokingAppDBContext.SaveChanges();
        }
    }
}
