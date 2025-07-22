using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly QuitSmokingAppDBContext _context;
        public ChatMessageRepository()
        {
            _context = new QuitSmokingAppDBContext();
        }

        public List<ChatMessage> GetAllByUserIdAndCoachId(Guid senderId, Guid coachId)
        { 
            return _context.ChatMessages
                .Where(cm => cm.SenderId == senderId && cm.ReceivedId == coachId || cm.SenderId == coachId && cm.ReceivedId == senderId)
                .ToList();
        }

        public List<ConversationDto> GetAllByUserId(Guid coachId)
        {
            return _context.ChatMessages
                .Where(m => m.ReceivedId == coachId)
                .GroupBy(m => m.SenderId)
                .Select(g => new ConversationDto
                {
                    SenderId = g.Key,
                    SenderName = g.First().Sender.FirstName + " " + g.First().Sender.LastName,
                    Timestamp = g.Max(m => m.SentAt)
                }).ToList();
        }


        public void SaveMessage(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            _context.SaveChanges();
        }
    }
}
