using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class JobUpdateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Position { get; set; }
        public long? Amount { get; set; }
    }
}
