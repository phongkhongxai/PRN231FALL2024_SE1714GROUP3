using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class InterviewRoundUpdateDTO
    {
        public int? RoundNumber { get; set; }
        public string? RoundName { get; set; }
        public string? Description { get; set; }
    }
}
