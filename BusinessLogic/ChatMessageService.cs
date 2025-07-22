using BusinessLogic.IServices;
using BusinessObject.DTOs;
using BusinessObject.Models;
using DataAccess;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ChatMessageService : IChatMessageService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatMessageService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public List<ConversationDto> GetAllByUserId(Guid coachId)
        {
            return _chatMessageRepository.GetAllByUserId(coachId);
        }

        public List<ChatMessage> GetAllByUserIdAndCoachId(Guid senderId, Guid coachId)
        {
            return _chatMessageRepository.GetAllByUserIdAndCoachId(senderId, coachId);
        }

        public void SaveMessage(ChatMessage chatMessage)
        {
            _chatMessageRepository.SaveMessage(chatMessage);
        }
    }
}
