using BusinessObjects.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DbContenxt
{
    public  class SkillDAO
    {
        private static SkillDAO instance;
        public static SkillDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SkillDAO();
                }
                return instance;
            }
        } 

         
        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            var _context = new RecuitmentDbContext();
            return await _context.Skills
                .Where(s => !s.IsDelete)
                .ToListAsync();
        }

         
        public async Task<Skill> GetSkillByIdAsync(long id)
        {
            var _context = new RecuitmentDbContext();
            return await _context.Skills
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDelete);
        }
         
        public async Task<Skill> AddSkillAsync(Skill skill)
        {
            var _context = new RecuitmentDbContext();
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
            return skill;
        }
         
        public async Task<Skill> UpdateSkillAsync(Skill skill)
        {
            var _context = new RecuitmentDbContext();

            var existingSkill = await GetSkillByIdAsync(skill.Id);
            if (existingSkill == null) return null;

            existingSkill.Name = skill.Name;
            existingSkill.Type = skill.Type;


            _context.Skills.Update(existingSkill);
            await _context.SaveChangesAsync();
            return existingSkill;
        }
         
        public async Task<bool> DeleteSkillAsync(long id)
        {
            var _context = new RecuitmentDbContext();

            var skill = await GetSkillByIdAsync(id);
            if (skill == null) return false;

            skill.IsDelete = true; 
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

