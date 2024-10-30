using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.DbContenxt;
using DAL.Repositories;
using DAL.Repositories.Impl;

namespace Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserResponseDTO> GetUserById(long id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            return mapper.Map<UserResponseDTO>(user);
        }

        public async Task<List<UserResponseDTO>> GetAllUsers()
        {
            var user = await userRepository.GetAllUsers();
            return mapper.Map<List<UserResponseDTO>>(user);
        }

        public async Task<UserDTO> UpdateUser(long id, UserUpdateDTO userDTO)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                if (userDTO.Username != null)
                {
                    user.Username = userDTO.Username;
                }
                if (userDTO.Address != null)
                {
                    user.Address = userDTO.Address;
                }
                if (userDTO.Phone != null)
                {
                    user.Phone = userDTO.Phone;
                }
                if (userDTO.Gender != null)
                {
                    user.Gender = userDTO.Gender;
                }
                if (userDTO.RoleId != null)
                {
                    user.RoleId = userDTO.RoleId;
                }
                if (userDTO.IsDelete != null)
                {
                    user.IsDelete = userDTO.IsDelete;
                }
                //user.UserSkills.Clear();
                //foreach (var skill in userDTO.SkillIds)
                //{
                //    await userRepository.AddUserSkill(user.Id, skill.SkillId, skill.Experiences);
                //}
                foreach (var skillToAdd in userDTO.SkillIds)
                {
                    // Kiểm tra nếu skill đã tồn tại trong job.JobSkills hay không
                    if (!user.UserSkills.Any(s => s.SkillId == skillToAdd.SkillId))
                    {
                        // Nếu skill chưa có trong job, thêm nó vào
                        await userRepository.AddUserSkill(user.Id, skillToAdd.SkillId, skillToAdd.Experiences);
                    }
                }

                // Nếu cần xóa các skill không có trong SkillsToAdd, dùng Except
                var skillsToRemove = user.UserSkills
                    .Where(s => !userDTO.SkillIds.Any(sa => sa.SkillId == s.SkillId))
                    .ToList();

                // Xóa các skill cần loại bỏ
                foreach (var skill in skillsToRemove)
                {
                    await userRepository.RemoveSkill(user.Id, skill.SkillId);
                }


                var updated = await userRepository.UpdateUser(user);
                return mapper.Map<UserDTO>(updated);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteUser(long id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }
            return await userRepository.DeleteUser(id);

        }

        public async Task<UserDTO> FindByEmail(string email)
        {
            var user = await userRepository.FindByEmail(email);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<bool> ChangePassword(long id, ChangePasswordDTO changePasswordDTO)
        {
            return await userRepository.ChangePassword(id, changePasswordDTO.currentPassword, changePasswordDTO.newPassword);
        }
    }
}
