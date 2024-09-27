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
            CreateMap<Job, JobDTO>(); 
            CreateMap<JobCreateDTO, Job>();
            CreateMap<JobUpdateDTO, Job>();

        }
    }
}
