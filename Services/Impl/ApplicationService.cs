using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using DAL.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class ApplicationService : IApplicationService
    {
        IApplicationRepository repository;
        IJobRepository jobRepository;
        IUserRepository userRepository;
        private readonly IMapper mapper;

        public ApplicationService(IApplicationRepository repository, IJobRepository jobRepository, IUserRepository userRepository, IMapper mapper)
        {
            this.repository = repository;
            this.jobRepository = jobRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<ApplicationDTO> CreateApplicationAsync(ApplicationCreateDTO applicationCreateDTO)
        {
            var application = mapper.Map<Application>(applicationCreateDTO);
            var user = await userRepository.GetUserByIdAsync(applicationCreateDTO.UserId);
            var job = await jobRepository.GetJobByIdAsync(applicationCreateDTO.JobId);
            if (job == null || user == null) return null;
            var created = await repository.CreateApplicationAsync(application);
            return mapper.Map<ApplicationDTO>(created);
        }

        public async Task<bool> DeleteApplicationAsync(long id)
        {
            var application = await repository.GetApplicationByIdAsync(id);
            if (application == null)
            {
                return false;
            }
            return await repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync()
        {
            var applications = await repository.GetAllApplicationsAsync();
            return mapper.Map<IEnumerable<ApplicationDTO>>(applications);
        }

        public async Task<ApplicationDTO> GetApplicationByIdAsync(long id)
        {
            var application = await repository.GetApplicationByIdAsync(id);
            return mapper.Map<ApplicationDTO>(application);
        }

        public async Task<IEnumerable<ApplicationDTO>> GetApplicationByUserIdAsync(long id)
        {
            var applications = await repository.GetApplicationByUserIdAsync(id);
            return mapper.Map<IEnumerable<ApplicationDTO>>(applications);
        }

        public async Task<ApplicationDTO> UpdateApplicationAsync(long id, ApplicationUpdateDTO applicationUpdateDTO)
        {
            var application = await repository.GetApplicationByIdAsync(id);
            if (application == null) return null;
            if (!string.IsNullOrEmpty(applicationUpdateDTO.Status))
            {
                application.Status= applicationUpdateDTO.Status;
            }

            var updated = await repository.UpdateApplicationAsync(application);
            return mapper.Map<ApplicationDTO>(updated);
        }
    }
}
