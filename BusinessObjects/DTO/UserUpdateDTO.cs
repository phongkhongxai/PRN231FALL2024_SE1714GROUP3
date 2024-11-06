using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class UserUpdateDTO
    {
        //public string? Email { get; set; }
        public string Username { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public bool IsDelete { get; set; } = false;
        public long RoleId { get; set; }
        public List<SkillAddDTO>? SkillIds { get; set; }
    }
}
