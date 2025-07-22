using BusinessLogic.IServices;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public void Create(Feedback feedback)
        {
            _feedbackRepository.Create(feedback);
        }

        public List<CoachDto> GetAll()
        {
            return _feedbackRepository.GetAll();
        }

        public List<Feedback> GetByCoachId(Guid coachId)
        {
            return _feedbackRepository.GetByCoachId(coachId);
        }
    }
}
