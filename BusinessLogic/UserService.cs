using BusinessLogic.IServices;
using BusinessObject.Models;
using DataAccess;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly IUserRepostitory _userRepostitory;
   
        public UserService(IUserRepostitory userRepostitory )
        {
            _userRepostitory = userRepostitory;
           
        }
        public async Task<List<User>> GetAll()
        {
            return await _userRepostitory.GetAllAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _userRepostitory.GetByGuiUser(id);
        }

        public User Login(string username, string password)
        {
            return _userRepostitory.Login(username, password);
        }

        public void Register(User user)
        {
            _userRepostitory.Register(user);
        }

        public void UpdateProfile(User user)
        {
            _userRepostitory.UpdateProfile(user);
        }

        // Admin 
        public async Task<List<User>> GetAllUsers()
        {

            return await _userRepostitory.GetAllUsers();

        }


    }
}
