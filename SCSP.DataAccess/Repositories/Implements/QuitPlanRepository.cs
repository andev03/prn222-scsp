using BusinessObjects.Models.Entities;
using Microsoft.EntityFrameworkCore;
using SCSP.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SCSP.DataAccess.Repositories.Implements
{
    public class QuitPlanRepository : CrudRepository<QuitPlan>, IQuitPlanRepository
    {
        public QuitPlanRepository(DbContext context) : base(context)
        {
        }
    
    }
}
