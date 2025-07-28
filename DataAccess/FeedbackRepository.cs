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
                .AsEnumerable()
                .GroupBy(f => new { f.CoachId, f.Coach.FirstName, f.Coach.LastName })
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


        // Admin
        public async Task<IEnumerable<Feedback>> GetAllAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetByRatingAsync(byte rating)
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .Where(f => f.Rating == rating)
                .OrderByDescending(f => f.CreatedAt)
                .ToListAsync();
        }

        public async Task<Feedback> GetByIdAsync(int id)
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);
        }

        public async Task DeleteAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }

    }
}
