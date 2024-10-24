using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Entity
{
    public class Resume
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserId { get; set; }
        public bool IsDelete { get; set; } = false;
        public string FilePath { get; set; }
        public User User { get; set; }

        // Sử dụng JSON để lưu thông tin kinh nghiệm, dự án, kỹ năng
        public string? ContactInfoJson { get; set; } // Lưu dưới dạng JSON
        public string? ExperiencesJson { get; set; } // Kinh nghiệm
        public string? ProjectsJson { get; set; } // Dự án
        public string? SkillsJson { get; set; } // Kỹ năng
        public string? EducationJson { get; set; } // Thông tin giáo dục
        public string? CertificationsJson { get; set; } // Chứng chỉ
        public ICollection<Application> Applications { get; set; } = new List<Application>();

    }
}
