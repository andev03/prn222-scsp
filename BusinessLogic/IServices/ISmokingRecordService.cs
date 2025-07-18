using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface ISmokingRecordService
    {

        Task<List<SmokingRecord>> GetAll();
        Task<SmokingRecord> GetById(int RecordId);
        Task<int> Create(SmokingRecord item);
        Task<int> Update(SmokingRecord item);
        Task<int> SoftDelete(int RecordId);

        List<SmokingRecord> GetAllByUserId(Guid userId);
    }
}
