using BusinessObjects.DTO;
using BusinessObjects.Entity;
using AutoMapper;
using BusinessObjects.DTOs;

namespace Services.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<UserResponseDTO, User>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<Job, JobDTO>()

            .ForMember(dest => dest.JobSkills, opt => opt.MapFrom(src => src.JobSkills.Select(js => new JobSkillDTO
            {
                SkillId = js.SkillId,
                SkillName = js.Skill.Name,
                Experiences = js.Experiences
            })));

            CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.UserSkills, opt => opt.MapFrom(src => src.UserSkills.Select(js => new UserSkillDTO
            {
                SkillId = js.SkillId,
                SkillName = js.Skill.Name,
                Experiences = js.Experiences
            })));

            //CreateMap<Application, ApplicationDTO>()
            //    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            //    .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job))
            //    //.ForMember(dest => dest.Resume, opt => opt.MapFrom(src => src.Resume))
            //    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));

            CreateMap<Application, ApplicationDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Job, opt => opt.MapFrom(src => src.Job))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.ResponseResumeDTO, opt => opt.MapFrom(src => new ResponseResumeDTO
            {
                Id = src.Resume.Id,
                UserId = src.Resume.UserId,
                FilePath = src.Resume.FilePath
            }));

            CreateMap<JobCreateDTO, Job>();
            CreateMap<JobUpdatedDTO, Job>();
            CreateMap<SkillDTO, Skill>();
            CreateMap<Skill, SkillDTO>();
            CreateMap<InterviewRound, InterviewRoundDTO>();
            CreateMap<InterviewRoundCreateDTO, InterviewRound>();
            CreateMap<ApplicationCreateDTO, Application>();
            CreateMap<ApplicationUpdateDTO, Application>();
            CreateMap<Resume, ResponseResumeDTO>()
                .ReverseMap();

            CreateMap<InterviewSession, InterviewSessionDTO>()
                .ForMember(dest => dest.InterviewRound, opt => opt.MapFrom(src => src.InterviewRound))
                .ForMember(dest => dest.SessionApplications, opt => opt.MapFrom(src => src.SessionApplications))
                .ForMember(dest => dest.SessionInterviewers, opt => opt.MapFrom(src => src.SessionInterviewers));

            CreateMap<InterviewSessionCreateDTO, InterviewSession>();
            CreateMap<InterviewSessionUpdateDTO, InterviewSession>();

            // Mapping cho SessionApplication và SessionInterviewer
            CreateMap<SessionApplication, SessionApplicationDTO>()
                .ForMember(dest => dest.Application, opt => opt.MapFrom(src => src.Application));

            CreateMap<SessionInterviewer, SessionInterviewerDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));




        }
    }
}
