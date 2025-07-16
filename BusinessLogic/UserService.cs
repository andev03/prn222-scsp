using BusinessLogic.IServices;
using BusinessObject;
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
        public UserService(IUserRepostitory userRepostitory)
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
    }
}
