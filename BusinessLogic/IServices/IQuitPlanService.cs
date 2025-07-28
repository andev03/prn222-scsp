using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IService
{
    public interface IQuitPlanService
    {
        List<QuitPlan> GetAllByUserId(Guid userId);

        QuitPlan GetById(int planId);

        void Create(QuitPlan quitPlan);
        void Update(QuitPlan quitPlan);
        void Delete(QuitPlan quitPlan);


        Task<List<QuitPlan>> GetAllQuitPlanAsync();


        Task<List<QuitPlan>> GetQuitPlansByUserIdAsync(Guid userId);

        Task<QuitPlan> GetByIdAsync(int id);

        Task AddAsync(QuitPlan plan);

        Task UpdateAsync(QuitPlan plan);

        Task DeleteAsync(int id);
    }
}
