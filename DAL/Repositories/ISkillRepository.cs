using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ISkillRepository
    {
        Task<IEnumerable<Skill>> GetAllSkillsAsync();
        Task<Skill> GetSkillByIdAsync(long id);
        Task<Skill> AddSkillAsync(Skill skill);
        Task<Skill> UpdateSkillAsync(Skill skill);
        Task<bool> DeleteSkillAsync(long id);
    }
}
