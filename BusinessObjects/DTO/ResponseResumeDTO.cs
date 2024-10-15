using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ResponseResumeDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? FilePath { get; set; }
    }
}
