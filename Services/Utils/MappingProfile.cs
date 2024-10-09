using BusinessObjects.DTO;
using BusinessObjects.Entity; 
using AutoMapper;

namespace Services.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>(); 
            CreateMap<UserUpdateDTO, User>();
            CreateMap<Job, JobDTO>()
            .ForMember(dest => dest.JobSkills, opt => opt.MapFrom(src => src.JobSkills.Select(js => new JobSkillDTO
            {
                SkillId = js.SkillId,
                SkillName = js.Skill.Name,  
                Experiences = js.Experiences
            })));
            CreateMap<JobCreateDTO, Job>();
            CreateMap<JobUpdatedDTO, Job>();
            CreateMap<SkillDTO, Skill>();
            CreateMap<Skill, SkillDTO>();
            CreateMap<InterviewRound, InterviewRoundDTO>();
            CreateMap<InterviewRoundCreateDTO, InterviewRound>();





        }
    }
}
