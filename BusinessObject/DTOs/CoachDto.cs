using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs
{
    public class CoachDto
    {
        public Guid CoachID { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
    }
}
