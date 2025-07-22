using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IChatMessageRepository
    {
        void SaveMessage(ChatMessage chatMessage);

        List<ChatMessage>GetAllByUserIdAndCoachId(Guid senderId, Guid coachId);
    }
}
