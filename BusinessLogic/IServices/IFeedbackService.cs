using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IFeedbackService
    {
        List<CoachDto> GetAll();

        List<Feedback> GetByCoachId(Guid coachId);
        void Create(Feedback feedback);

    }
}
