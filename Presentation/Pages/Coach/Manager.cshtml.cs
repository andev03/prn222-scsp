using BusinessLogic.IServices;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Presentation.Pages.Coach
{
    public class ManagerModel : PageModel
    {
        private readonly IChatMessageService _chatMessage;
        public ManagerModel(IChatMessageService chatMessage)
        {
            _chatMessage = chatMessage;
        }

        public List<CoachDto> Coaches { get; set; }
        public List<ConversationDto> Messages { get; set; }
        public User User { get; set; }

        [BindProperty]
        public string NewMessage { get; set; }

        [BindProperty]
        public Guid CoachId { get; set; }

        public void OnGet()
        {
            User = HttpContext.Session.GetObject<User>("user");

            Messages = _chatMessage.GetAllByUserId(User.UserId);
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
    }
}
