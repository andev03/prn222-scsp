using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IBadgeService
    {
        Task<List<Badge>> GetAll();
        Task<Badge> GetById(int id);
        Task<int> Create(Badge badge);
        Task<int> Update(Badge badge);
        Task<int> SoftDelete(int badgeId);
   
    }
}
