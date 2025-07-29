using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
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



        Task<IEnumerable<Feedback>> GetAllAsync();

        Task<IEnumerable<Feedback>> GetByRatingAsync(byte rating);

        Task<Feedback> GetByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}
