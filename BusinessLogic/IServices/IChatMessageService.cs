using BusinessObject.DTOs;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IServices
{
    public interface IChatMessageService
    {
        void SaveMessage(ChatMessage chatMessage);

        List<ChatMessage> GetAllByUserIdAndCoachId(Guid senderId, Guid coachId);
        List<ConversationDto> GetAllByUserId(Guid coachId);
    }
}
