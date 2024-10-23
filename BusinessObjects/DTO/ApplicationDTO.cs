using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ApplicationDTO
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public UserDTO User { get; set; }
        public JobDTO Job { get; set; }
        public ResponseResumeDTO ResponseResumeDTO { get; set; }

    }
}
