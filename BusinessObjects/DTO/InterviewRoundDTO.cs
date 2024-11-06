using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class InterviewRoundDTO
    {
        public long Id { get; set; }
        public long JobId { get; set; }
        public int RoundNumber { get; set; }
        public string Status { get; set; }
        public string RoundName { get; set; }
        public string Description { get; set; }
    }
}
