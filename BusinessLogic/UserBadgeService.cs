using BusinessLogic.IServices;
using BusinessObject.DTOs;
using BusinessObject.Models;
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

        public List<NumberBadgeResponse> NumberBadgesReceived()
        {
            List<UserBadge> userBadges = _userBadgeRepository.GetAll();
            var result = userBadges
                .Where(b => b.IsDeleted != true && b.Disabled != true)
                .GroupBy(b => b.UserId)
                .Select(g => new NumberBadgeResponse
                {
                    User = g.First().User,
                    BadgeCount = g.Count()
                }).ToList();

            return result;
        }

        public async Task<int> SoftDelete(int UserBadgeId)
        {
            var item = await _userBadgeRepository.GetByIdAsync(UserBadgeId);
            item.IsDeleted = true;
            return await _userBadgeRepository.UpdateAsync(item);
        }

        public async Task<int> Update(UserBadge item)
        {
            return await _userBadgeRepository.UpdateAsync(item);
        }


        public async Task<List<UserBadge>> GetAllAsync()
        {

            return await _userBadgeRepository.GetAllAsync();

        }




        // Admin
        public async Task<UserBadge> GetByIdAsync(int id)
        {

            return await _userBadgeRepository.GetByIdAsync(id);

        }

        public async Task<List<UserBadge>> GetByUserIdAsync(Guid userId)
        {
            return await _userBadgeRepository.GetByUserIdAsync(userId);

        }


        public async Task AddBadgeAsync(UserBadge userBadge)
        {
            await _userBadgeRepository.AddBadgeAsync(userBadge);

        }
        public async Task DeleteBadgeAsync(int id)
        {

            await _userBadgeRepository.DeleteBadgeAsync(id);
        }


    }
}
