using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.DbContenxt;
using DAL.Repositories;
using DAL.Repositories.Impl;
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
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, ISkillRepository skillRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _skillRepository = skillRepository;
            _mapper = mapper;
        } 

        public async Task<JobDTO> CreateJobAsync(JobCreateDTO jobCreateDto)
        { 
            var job = _mapper.Map<Job>(jobCreateDto);
             
            var createdJob = await _jobRepository.CreateJobAsync(job);
            if (createdJob == null)
            {
                throw new Exception("Job creation failed.");
            }
             
            if (jobCreateDto.SkillsToAdd != null && jobCreateDto.SkillsToAdd.Any())
            {
                foreach (var skillDetail in jobCreateDto.SkillsToAdd)
                {
                    await _jobRepository.AddSkillToJobAsync(createdJob.Id, skillDetail.SkillId, skillDetail.Experiences);
                }
            }
             
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

        public async Task<JobDTO> UpdateJobAsync(long id, JobUpdatedDTO jobUpdatedDto)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (job == null)
            {
                return null;  
            }
             
            if (!string.IsNullOrEmpty(jobUpdatedDto.Title))
            {
                job.Title = jobUpdatedDto.Title;
            }

            if (!string.IsNullOrEmpty(jobUpdatedDto.Description))
            {
                job.Description = jobUpdatedDto.Description;
            }

            if (!string.IsNullOrEmpty(jobUpdatedDto.Position))
            {
                job.Position = jobUpdatedDto.Position;
            }

            // Update salary and amount if they are provided
            if (jobUpdatedDto.MinSalary.HasValue)
            {
                job.MinSalary = jobUpdatedDto.MinSalary.Value;
            }

            if (jobUpdatedDto.MaxSalary.HasValue)
            {
                job.MaxSalary = jobUpdatedDto.MaxSalary.Value;
            }

            if (jobUpdatedDto.Amount.HasValue)
            {
                job.Amount = jobUpdatedDto.Amount.Value;
            }

            // Handle adding new skills

            //job.JobSkills = [];
            foreach (var skillToAdd in jobUpdatedDto.SkillsToAdd)
            {
                // Kiểm tra nếu skill đã tồn tại trong job.JobSkills hay không
                if (!job.JobSkills.Any(s => s.SkillId == skillToAdd.SkillId))
                {
                    // Nếu skill chưa có trong job, thêm nó vào
                    await _jobRepository.AddSkillForJob(job.Id, skillToAdd.SkillId, skillToAdd.Experiences);
                }
            }

            // Nếu cần xóa các skill không có trong SkillsToAdd, dùng Except
            var skillsToRemove = job.JobSkills
                .Where(s => !jobUpdatedDto.SkillsToAdd.Any(sa => sa.SkillId == s.SkillId))
                .ToList();

            // Xóa các skill cần loại bỏ
            foreach (var skill in skillsToRemove)
            {
                await _jobRepository.RemoveSkillFromJobAsync(job.Id, skill.SkillId);
            }


            //if (jobUpdatedDto.SkillsToAdd != null)
            //{
            //    foreach (var skillToAdd in jobUpdatedDto.SkillsToAdd)
            //    {
            //        await _jobRepository.AddSkillToJobAsync(job.Id, skillToAdd.SkillId, skillToAdd.Experiences);
            //    }
            //}

            // Handle removing skills
            //if (jobUpdatedDto.SkillsToRemove != null)
            //{
            //    foreach (var skillId in jobUpdatedDto.SkillsToRemove)
            //    {
            //        await _jobRepository.RemoveSkillFromJobAsync(job.Id, skillId);
            //    }
            //}

            var updatedJob = await _jobRepository.UpdateJobAsync(job);
             
            return _mapper.Map<JobDTO>(updatedJob);
        }

         
    }
}
