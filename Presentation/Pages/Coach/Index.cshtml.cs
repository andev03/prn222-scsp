using BusinessLogic.IServices;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Presentation.Pages.Coach
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {

        private readonly IFeedbackService _feedbackService;
        private readonly IChatMessageService _chatMessage;
        public IndexModel(IFeedbackService feedbackService, IChatMessageService chatMessage)
        {
            _feedbackService = feedbackService;
            _chatMessage = chatMessage;
        }

        public List<CoachDto> Coaches { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public User User { get; set; }


        [BindProperty]
        public string NewMessage { get; set; }

        [BindProperty]
        public Guid CoachId { get; set; }

        public void OnGet()
        {
            User = HttpContext.Session.GetObject<User>("user");

            Coaches = _feedbackService.GetAll();
        }

        public IActionResult OnGetConnect(Guid coachId)
        {
            var user = HttpContext.Session.GetObject<User>("user");
            var messages = _chatMessage.GetAllByUserIdAndCoachId(user.UserId, coachId);

            var html = new System.Text.StringBuilder();

            foreach (var message in messages)
            {
                if (message.SenderId == user.UserId)
                {
                    html.Append($"<div class='message sent'>{message.MessageText}</div>");
                }
                else
                {
                    html.Append($"<div class='message received'>{message.MessageText}</div>");
                }
            }

            return Content(html.ToString(), "text/html");
        }

        public IActionResult OnPostSendMessage()
        {
            User = HttpContext.Session.GetObject<User>("user");

            if (!string.IsNullOrWhiteSpace(NewMessage))
            {
                ChatMessage message = new ChatMessage
                {
                    SenderId = User.UserId,
                    ReceivedId = CoachId,
                    MessageText = NewMessage
                };

                _chatMessage.SaveMessage(message);
            }

            return RedirectToPage();
        }
        public IActionResult OnGetReviews(Guid coachId)
        {
            var reviews = _feedbackService.GetByCoachId(coachId);

            var html = new System.Text.StringBuilder();

            if (reviews != null && reviews.Any())
            {
                foreach (var review in reviews)
                {
                    var reviewerName = review.User?.FirstName + " " + review.User?.LastName;

                    html.Append($@"
                <div class='review-item'>
                    <div class='review-author'><strong>{reviewerName}</strong></div>
                    <div class='review-rating'>
                        {new string('⭐', review.Rating)} <span>({review.Rating}/5)</span>
                    </div>
                    <p class='review-comment'>{review.Comment}</p>
                </div>");
                }
            }
            else
            {
                html.Append("<p class='no-reviews'>Chưa có đánh giá nào cho huấn luyện viên này.</p>");
            }

            return Content(html.ToString(), "text/html");
        }


        [BindProperty]
        public byte Rating { get; set; }

        [BindProperty]
        public string Comment { get; set; }

        public IActionResult OnPostRate()
        {
            var user = HttpContext.Session.GetObject<User>("user");

            if (user == null)
                return new JsonResult(new { success = false, message = "Bạn chưa đăng nhập." });

            var feedback = new Feedback
            {
                CoachId = CoachId,
                UserId = user.UserId,
                Rating = Rating,
                Comment = Comment
            };

            _feedbackService.Create(feedback); // phương thức lưu đánh giá vào DB

            return new JsonResult(new { success = true, message = "Đánh giá đã được gửi thành công!" });
        }
    }
}
