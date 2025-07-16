using BusinessObject;
using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IUserRepostitory  :IGenericRepository<User>
    {
        Task<User> GetByGuiUser(Guid id);
    }
}
