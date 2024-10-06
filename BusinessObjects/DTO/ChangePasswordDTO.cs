using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ChangePasswordDTO
    {
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
