using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class JobUpdatedDTO
    { 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Position { get; set; }
        public double? MinSalary { get; set; }
        public double? MaxSalary { get; set; }
        public long? Amount { get; set; }
        public List<SkillAddDTO>? SkillsToAdd { get; set; } = new List<SkillAddDTO>();
        public List<long>? SkillsToRemove { get; set; } = new List<long>();
    }
}
