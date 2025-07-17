using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class NumberBadgeResponse
    {
        public User User { get; set; }
        public int BadgeCount { get; set; }
    }
}
