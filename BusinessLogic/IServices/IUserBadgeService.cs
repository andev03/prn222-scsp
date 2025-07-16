using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IUserBadgeService
    {
        Task<List<UserBadge>> GetAll();
        Task<UserBadge> GetById(int UserBadgeId);
        Task<int> Create(UserBadge item);
        Task<int> Update(UserBadge item);
        Task<int> SoftDelete(int UserBadgeId);
  
    }
}
