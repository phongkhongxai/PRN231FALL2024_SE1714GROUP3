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

        public ContactInfoDTO? ContactInfo { get; set; }
        public List<WorkExperienceDTO>? WorkExperience { get; set; }
        public List<ProjectDTO>? Project { get; set; }
        public List<EducationDTO>? Education { get; set; }

        public List<SkillsDTO>? Skills { get; set; }
        public List<AwardAndCertificateDTO>? AwardAndCertificate { get; set; }
    }
}
