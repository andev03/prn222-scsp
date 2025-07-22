using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public FeedbackRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public void Create(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public List<CoachDto> GetAll()
        {
            return _context.Feedbacks
                .Include(f => f.Coach)
                .AsEnumerable() // chuyển sang client-side để tránh lỗi nullable navigation
                .GroupBy(f => new { f.CoachId, f.Coach.FirstName, f.Coach.LastName }) // group theo coach thật sự
                .Select(g => new CoachDto
                {
                    CoachID = g.Key.CoachId,
                    Name = g.Key.FirstName + " " + g.Key.LastName,
                    Rating = Math.Round(g.Average(f => f.Rating), 1)
                }).ToList();
        }

        public List<Feedback> GetByCoachId(Guid coachId)
        {
            return _context.Feedbacks
                .Where(f => f.CoachId == coachId)
                .Include(f => f.Coach)
                .Include(f => f.User)
                .ToList();
        }

    }
}
