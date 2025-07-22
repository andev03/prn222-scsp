using BusinessLogic.IService;
using BusinessObject.Models;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class QuitPlanService : IQuitPlanService
    {
        private IQuitPlanRepository _repository;
        public QuitPlanService(IQuitPlanRepository repository)
        {
            _repository = repository;
        }

        public List<QuitPlan> GetAllByUserId(Guid userId)
        {
            return _repository.GetAllByUserId(userId);
        }

        public QuitPlan GetById(int planId)
        {
            return _repository.GetById(planId);
        }

        public void Create(QuitPlan quitPlan)
        {
            _repository.Create(quitPlan);
        }
        public void Update(QuitPlan quitPlan)
        {
            _repository.Update(quitPlan);
        }

        public void Delete(QuitPlan quitPlan)
        {
            _repository.Delete(quitPlan);
        }

    }
}
