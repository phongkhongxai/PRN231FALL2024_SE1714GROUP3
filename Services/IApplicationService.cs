using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IApplicationService
    {
        public Task<IEnumerable<ApplicationDTO>> GetAllApplicationsAsync();
        public Task<ApplicationDTO> GetApplicationByIdAsync(long id);
        public Task<IEnumerable<ApplicationDTO>> GetApplicationByUserIdAsync(long id);
        public Task<ApplicationDTO> CreateApplicationAsync(ApplicationCreateDTO applicationCreateDTO);
        public Task<ApplicationDTO> UpdateApplicationAsync(long id, ApplicationUpdateDTO applicationUpdateDTO);
        public Task<bool> DeleteApplicationAsync(long id);

    }
}
