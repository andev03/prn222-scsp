using BusinessObject;
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
      
    }
}
