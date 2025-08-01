﻿using BusinessObject.Models;
using DataAccess.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface ISmokingRecordRepository : IGenericRepository<SmokingRecord>
    {
        List<SmokingRecord> GetAllByUserId(Guid userId);
    }
}
