using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class JobDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double MinSalary { get; set; }
        public double MaxSalary { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Position { get; set; }
        public long Amount { get; set; }
        public IEnumerable<JobSkillDTO> JobSkills { get; set; }
        public ICollection<InterviewRound> InterviewRounds { get; set; } = new List<InterviewRound>();

    }
}
