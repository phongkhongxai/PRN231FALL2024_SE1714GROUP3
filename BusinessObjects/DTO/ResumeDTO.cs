using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO
{
    public class ResumeDTO
    {
        public string Name {  get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        //Mục tiêu nghề nghiệp
        public string CareerGoal { get; set; }
        public List<WorkExperienceDTO>? WorkExperience { get; set; }
        public List<ProjectDTO>? Project { get; set; }
        public List<EducationDTO>? Education { get; set; }

        public List<SkillsDTO>? Skills { get; set; }
        public List<AwardAndCertificateDTO>? AwardAndCertificate { get; set; }

        public string MoreInfomation { get; set; }

    }

    public class ContactInfoDTO 
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CareerGoal { get; set; }
        public string MoreInfomation { get; set; }
    }

    public class WorkExperienceDTO
    {
        public string? NameCompany { get; set; }
        public string? TimeAtCompany { get; set; }
        public string? PositionAtCompany { get; set; }
        public string? Description { get; set; }

    }

    public class ProjectDTO 
    { 
        public string? NameProject { get; set; }
        public string? TimeAtWork { get; set; }
        public string? PositionInProject { get; set; }
        public string? TechsUsed { get; set; }
    }

    public class EducationDTO 
    { 
        public string? NameUniversity { get; set; }
        public string? TimeAtLearn { get; set; }
        public string? Major { get; set; }

    }

    public class SkillsDTO
    {
        public string? NameSkill { get; set; }
        public string? Description { get; set; }
    }

    public class AwardAndCertificateDTO
    {
        public string? Time {  get; set; }
        public string? Name { get; set; }
    }
}
