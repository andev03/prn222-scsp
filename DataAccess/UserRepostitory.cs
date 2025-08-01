﻿using BusinessObject.Models;
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
    public class UserRepostitory : GenericRepository<User>, IUserRepostitory
    {
        private readonly QuitSmokingAppDBContext _quitSmokingAppDBContext;
        public UserRepostitory()
        {
            _quitSmokingAppDBContext = new QuitSmokingAppDBContext();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Select(u => new User
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                FirstName = u.FirstName,
                LastName = u.LastName,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToListAsync();
        }

        public List<User> GetAllByRole()
        {
            return _quitSmokingAppDBContext.Users.Where(u => u.Role.ToLower() == "coach").ToList();
        }

        public async Task<User> GetByGuiUser(Guid id)
        {
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

        public async Task UpdateRoleAsync(Guid userId, string newRole)
        {
            using (var context = new QuitSmokingAppDBContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user != null)
                {
                    user.Role = newRole;
                    user.UpdatedAt = DateTime.UtcNow;
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
