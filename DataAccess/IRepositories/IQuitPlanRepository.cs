using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IQuitPlanRepository
    {
        List<QuitPlan> GetAllByUserId(Guid userId);

        QuitPlan GetById(int planId);

        void Create(QuitPlan quitPlan);

        void Update(QuitPlan quitPlan);
        void Delete(QuitPlan quitPlan);
    }
}
