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
    public class BadgeService : IBadgeService
    {
        private IBadgeRepository _repository;
        public BadgeService(IBadgeRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Create(Badge badge)
        {
            return await _repository.AddAsync(badge);
        }

        public async Task<List<Badge>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Badge> GetById(int id)
        {
          return await _repository.GetByIdAsync(id);
        }

        public Task<bool> IsExist(int id)
        {
           return _repository.IsExist(id);
        }

        public async Task<int> SoftDelete(int badgeId)
        {
            var item = await _repository.GetByIdAsync(badgeId);
            item.IsDeleted = true;
            item.Disabled = true;
            return await _repository.UpdateAsync(item);
        }

        public Task<int> Update(Badge badge)
        {
           return _repository.UpdateAsync(badge);
        }
    }
}
