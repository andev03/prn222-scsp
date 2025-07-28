using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(Guid id);
        User Login(String username, string password);
        void Register(User user);

        void UpdateProfile(User user);

        Task<List<User>> GetAllUsers();
    }
}
