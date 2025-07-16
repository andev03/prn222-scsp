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
    public class UserBadgeService : IUserBadgeService
    {
        private readonly IUserBadgeRepository _userBadgeRepository;
        public UserBadgeService(IUserBadgeRepository userBadgeRepository)
        {
            _userBadgeRepository = userBadgeRepository;
        }
        public async Task<int> Create(UserBadge item)
        {
           return await _userBadgeRepository.AddAsync(item);
        }

        public async Task<List<UserBadge>> GetAll()
        {
            return await _userBadgeRepository.GetAllDetails();
        }

        public async Task<UserBadge> GetById(int UserBadgeId)
        {
           return await _userBadgeRepository.GetByIdDetails(UserBadgeId);
        }

    

        public async Task<int> SoftDelete(int UserBadgeId)
        {
            var item = await _userBadgeRepository.GetByIdAsync(UserBadgeId);
            item.IsDeleted = true;
            item.Disabled = true;
            return await _userBadgeRepository.UpdateAsync(item);
        }

        public async Task<int> Update(UserBadge item)
        {
            return await _userBadgeRepository.UpdateAsync(item);
        }
    }
}
