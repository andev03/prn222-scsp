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
        private readonly IUserRepostitory _userRepostitory;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUserRepostitory userRepostitory)
        {
            _feedbackRepository = feedbackRepository;
            _userRepostitory = userRepostitory;
        }

        public void Create(Feedback feedback)
        {
            _feedbackRepository.Create(feedback);
        }

        public List<CoachDto> GetAll()
        {
            List<User> users = _userRepostitory.GetAllByRole(); // List<User> có Role = "coach"
            List<CoachDto> coachDtos = _feedbackRepository.GetAll(); // List<CoachDto> đã có feedback
            List<CoachDto> result = new List<CoachDto>();

            // Lấy danh sách các CoachID đã có feedback
            var coachIdsWithFeedback = coachDtos.Select(c => c.CoachID).ToHashSet();

            // Lặp qua tất cả user, nếu chưa có feedback thì thêm vào result
            foreach (var user in users)
            {
                if (!coachIdsWithFeedback.Contains(user.UserId))
                {
                    coachDtos.Add(new CoachDto
                    {
                        CoachID = user.UserId,
                        Name = user.FirstName + " " + user.LastName,
                        Rating = 0 // hoặc null nếu muốn phân biệt
                    });
                }
            }
            return coachDtos;
        }

        public List<Feedback> GetByCoachId(Guid coachId)
        {
            return _feedbackRepository.GetByCoachId(coachId);
        }
    }
}
