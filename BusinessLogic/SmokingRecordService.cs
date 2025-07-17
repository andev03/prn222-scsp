using BusinessLogic.IServices;
using BusinessObject.Models;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    partial class SmokingRecordService : ISmokingRecordService
    {
        private readonly ISmokingRecordRepository _repository;
        public SmokingRecordService(ISmokingRecordRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> Create(SmokingRecord item)
        {
            return await _repository.AddAsync(item);   
        }

        public async Task<List<SmokingRecord>> GetAll()
        {
           return await _repository.GetAllAsync();
        }

        public async Task<SmokingRecord> GetById(int RecordId)
        {
           return await _repository.GetByIdAsync(RecordId);
        }

      

        public async Task<int> SoftDelete(int RecordId)
        {
            var item = await _repository.GetByIdAsync(RecordId);
            item.IsDeleted = true;
            return await _repository.UpdateAsync(item);
        }

        public async Task<int> Update(SmokingRecord item)
        {
            return await _repository.UpdateAsync(item);
        }
    }
}
