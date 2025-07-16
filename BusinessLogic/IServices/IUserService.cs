using BusinessObject;
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
    }
}
