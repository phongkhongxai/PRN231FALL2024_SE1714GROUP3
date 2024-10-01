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
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;
        private readonly IMapper _mapper;

        public SkillService(ISkillRepository skillRepository, IMapper mapper)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
        }

        public async Task<SkillDTO> CreateSkillAsync(SkillDTO skillCreateDto)
        {
            var skill = _mapper.Map<Skill>(skillCreateDto);
            var createdSkill = await _skillRepository.AddSkillAsync(skill);
            return _mapper.Map<SkillDTO>(createdSkill);
        }

        public async Task<bool> DeleteSkillAsync(long id)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return false;
            }
            return await _skillRepository.DeleteSkillAsync(id);
        }

        public async Task<IEnumerable<SkillDTO>> GetAllSkillsAsync()
        {
            var skills = await _skillRepository.GetAllSkillsAsync();
            return _mapper.Map<IEnumerable<SkillDTO>>(skills);
        }

        public async Task<SkillDTO> GetSkillByIdAsync(long id)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);
            return _mapper.Map<SkillDTO>(skill);
        }

        public async Task<SkillDTO> UpdateSkillAsync(long id, SkillDTO skillUpdateDto)
        {
            var skill = await _skillRepository.GetSkillByIdAsync(id);
            if (skill == null)
            {
                return null;
            }

            // Kiểm tra và cập nhật từng trường
            if (skillUpdateDto.Name != null)
            {
                skill.Name = skillUpdateDto.Name;
            }

            var updatedSkill = await _skillRepository.UpdateSkillAsync(skill);
            return _mapper.Map<SkillDTO>(updatedSkill);
        }
    }
}
