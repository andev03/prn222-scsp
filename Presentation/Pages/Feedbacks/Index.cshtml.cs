using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using BusinessLogic.IServices;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessLogic;

namespace Presentation.Pages.Feedbacks
{
    public class IndexModel : PageModel
    {
        private readonly IFeedbackService _context;

        public IndexModel(IFeedbackService context)
        {
            _context = context;
        }

        public IList<Feedback> Feedback { get; set; } = default!;
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        public async Task OnGetAsync(int? page, int? rating)
        {
            int pageSize = 10;
            CurrentPage = page ?? 1;

            var allFeedback = await _context.GetAllAsync();

            if (rating.HasValue)
            {
                allFeedback = allFeedback.Where(f => f.Rating == rating.Value).ToList();
            }

            TotalPages = (int)Math.Ceiling(allFeedback.Count() / (double)pageSize);
            Feedback = allFeedback
                        .Skip((CurrentPage - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Tạo dropdown đánh giá
            ViewData["Ratings"] = new SelectList(
       new List<byte> { 1, 2, 3, 4, 5 },
       rating
             );
        }

    }
}
