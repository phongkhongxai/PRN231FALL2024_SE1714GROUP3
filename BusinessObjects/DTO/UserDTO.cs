using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool IsDelete { get; set; } = false;
        public string? Gender { get; set; }

        public long RoleId { get; set; }
    }
}
