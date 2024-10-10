using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public interface IResumeRepository
    {
        Task<IEnumerable<Resume>> GetAllResumesAsync();
        Task<Resume> GetResumeByIdAsync(long id);
        Task<Resume> CreateResumeAsync(Resume resume);
        Task<bool> DeleteResumeAsync(long id);
        Task<IEnumerable<Resume>> GetAllResumesByUserAsync(long UserId);

    }
    public class ResumeRepository : IResumeRepository
    {
        public async Task<IEnumerable<Resume>> GetAllResumesAsync()
        {
            return await ResumeDAO.Instance.GetAllAsync();
        }

        public async Task<IEnumerable<Resume>> GetAllResumesByUserAsync(long UserId)
        {
            return await ResumeDAO.Instance.GetByIdUserAsync(UserId);
        }


        public async Task<Resume> GetResumeByIdAsync(long id)
        {
            return await ResumeDAO.Instance.GetByIdAsync(id);
        }

        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            return await ResumeDAO.Instance.AddAsync(resume);
        }

        public async Task<bool> DeleteResumeAsync(long id)
        {
            return await ResumeDAO.Instance.DeleteAsync(id);
        }
    }
}
