using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }
        public async Task<JobDTO> CreateJobAsync(JobCreateDTO jobCreateDto)
        {
            var job = _mapper.Map<Job>(jobCreateDto); 
            var createdJob = await _jobRepository.CreateJobAsync(job);
            return _mapper.Map<JobDTO>(createdJob);
        }

        public async Task<bool> DeleteJobAsync(long id)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null)
            {
                return false;
            }
            return await _jobRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<JobDTO>> GetAllJobsAsync()
        {
            var jobs = await _jobRepository.GetAllJobsAsync();
            return _mapper.Map<IEnumerable<JobDTO>>(jobs);
        }

        public async Task<JobDTO> GetJobByIdAsync(long id)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            return _mapper.Map<JobDTO>(job);
        }

        public async Task<JobDTO> UpdateJobAsync(long id, JobUpdateDTO jobCreateDto)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null)
            {
                return null;
            }

            // Kiểm tra và cập nhật từng trường
            if (jobCreateDto.Title != null)
            {
                job.Title = jobCreateDto.Title;
            }
            if (jobCreateDto.Description != null)
            {
                job.Description = jobCreateDto.Description;
            }
            if (jobCreateDto.Position != null)
            {
                job.Position = jobCreateDto.Position;
            }
            if (jobCreateDto.Amount.HasValue)
            {
                job.Amount = jobCreateDto.Amount.Value;
            }

            var updatedJob = await _jobRepository.UpdateJobAsync(job);
            return _mapper.Map<JobDTO>(updatedJob);
        }

    }
}
