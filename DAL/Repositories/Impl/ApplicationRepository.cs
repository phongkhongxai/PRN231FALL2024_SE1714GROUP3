using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class ApplicationRepository : IApplicationRepository
    {
        public Task<Application> CreateApplicationAsync(Application application) => ApplicationDAO.Instance.AddAsync(application);

        public Task<bool> DeleteAsync(long id)=> ApplicationDAO.Instance.DeleteAsync(id);

        public Task<IEnumerable<Application>> GetAllApplicationsAsync()=> ApplicationDAO.Instance.GetAllAsync();

        public Task<Application> GetApplicationByIdAsync(long id)=> ApplicationDAO.Instance.GetByIdAsync(id);

        public Task<IEnumerable<Application>> GetApplicationByUserIdAsync(long id)=> ApplicationDAO.Instance.GetByUserIdAsync(id);
        public Task<IEnumerable<Application>> GetApplicationByJobIdAsync(long id) => ApplicationDAO.Instance.GetByJobIdAsync(id);

        public Task<Application> UpdateApplicationAsync(Application application) => ApplicationDAO.Instance.UpdateAsync(application);
    }
}
