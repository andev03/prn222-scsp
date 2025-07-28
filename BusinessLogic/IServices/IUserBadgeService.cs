using BusinessObject.DTOs;
using BusinessObject.Models;
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

        List<NumberBadgeResponse> NumberBadgesReceived();


        Task<List<UserBadge>> GetAllAsync();


        Task<UserBadge> GetByIdAsync(int id);

        Task<List<UserBadge>> GetByUserIdAsync(Guid userId);


        Task AddBadgeAsync(UserBadge userBadge);
        Task DeleteBadgeAsync(int id);

    }
}
