using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IFeedbackRepository
    {
        List<CoachDto> GetAll();
        List<Feedback> GetByCoachId(Guid coachId);

        void Create(Feedback feedback);
    }
}
