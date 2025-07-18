using BusinessObject.Models;
using DataAccess.Base;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SmokingRecordRepository : GenericRepository<SmokingRecord>, ISmokingRecordRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public SmokingRecordRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public List<SmokingRecord> GetAllByUserId(Guid userId)
        {
            return _context.SmokingRecords.Where(sr => sr.UserId == userId && sr.IsDeleted != true && sr.Disabled != true).ToList();
        }
    }
}
