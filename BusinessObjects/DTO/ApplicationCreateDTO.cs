using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ApplicationCreateDTO
    {
        public long JobId { get; set; }
        public long UserId { get; set; }
        //public string? Status { get; set; }
        public long ResumeId { get; set; }

    }
}
