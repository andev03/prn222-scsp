using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class ConversationDto
    {
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
