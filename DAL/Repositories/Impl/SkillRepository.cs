using BusinessObjects.Entity;
using DAL.DbContenxt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Impl
{
    public class SkillRepository : ISkillRepository
    {
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            return await SkillDAO.Instance.AddSkillAsync(skill);
        }

        public async Task<bool> DeleteSkillAsync(long id)
        {
            return await SkillDAO.Instance.DeleteSkillAsync(id);
        }

        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await SkillDAO.Instance.GetAllSkillsAsync();
        }

        public async Task<Skill> GetSkillByIdAsync(long id)
        {
            return await SkillDAO.Instance.GetSkillByIdAsync(id);
        }

        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            return await SkillDAO.Instance.UpdateSkillAsync(skill);
        }
    }
}
