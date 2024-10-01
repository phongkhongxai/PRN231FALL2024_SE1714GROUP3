using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDTO>> GetAllSkillsAsync();
        Task<SkillDTO> GetSkillByIdAsync(long id);
        Task<SkillDTO> CreateSkillAsync(SkillDTO skillCreateDto);
        Task<SkillDTO> UpdateSkillAsync(long id, SkillDTO skillUpdateDto);
        Task<bool> DeleteSkillAsync(long id);
    }
}
