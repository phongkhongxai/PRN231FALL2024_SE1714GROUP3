using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IJobService
    {
        Task<IEnumerable<JobDTO>> GetAllJobsAsync();
        Task<JobDTO> GetJobByIdAsync(long id);
        Task<JobDTO> CreateJobAsync(JobCreateDTO jobCreateDto);
        Task<JobDTO> UpdateJobAsync(long id, JobUpdateDTO jobCreateDto);
        Task<bool> DeleteJobAsync(long id);
    }
}
