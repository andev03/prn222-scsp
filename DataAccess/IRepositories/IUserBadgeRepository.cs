using BusinessObject.Models;
using DataAccess.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IUserBadgeRepository : IGenericRepository<UserBadge>
    {
        Task<List<UserBadge>> GetAllDetails();
        Task<UserBadge> GetByIdDetails(int id);
        List<UserBadge> GetAll();


        Task<List<UserBadge>> GetAllAsync();


        Task<UserBadge> GetByIdAsync(int id);

        Task<List<UserBadge>> GetByUserIdAsync(Guid userId);


        Task AddBadgeAsync(UserBadge userBadge);
        Task DeleteBadgeAsync(int id);
    }
}
