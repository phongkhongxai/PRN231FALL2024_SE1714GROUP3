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

            CreateMap<Application, ApplicationDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job))
                //.ForMember(dest => dest.Resume, opt => opt.MapFrom(src => src.Resume))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<JobCreateDTO, Job>();
            CreateMap<JobUpdatedDTO, Job>();
            CreateMap<SkillDTO, Skill>();
            CreateMap<Skill, SkillDTO>();
            CreateMap<InterviewRound, InterviewRoundDTO>();
            CreateMap<InterviewRoundCreateDTO, InterviewRound>();
            CreateMap<ApplicationCreateDTO, Application>();
            CreateMap<ApplicationUpdateDTO, Application>();
            CreateMap<Resume, ResponseResumeDTO>().ReverseMap();
            //    .ForMember(dest => dest.Applications, 
            //    opt => opt.MapFrom(src => src.Applications.Select(sl => new ApplicationDTO
            //{
            //    Id = sl.Id,
            //    Status=sl.Status,
            //    User = new UserDTO {
                    
            //    })));





        }
    }
}
