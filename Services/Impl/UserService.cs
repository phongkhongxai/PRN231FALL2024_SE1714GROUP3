using AutoMapper;
using BusinessObjects.DTO;
using BusinessObjects.Entity;
using DAL.DbContenxt;
using DAL.Repositories;
using DAL.Repositories.Impl;
using System.Security.Cryptography;
using System.Text;

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
                if (userDTO.RoleId != 0)
                {
                    user.RoleId = userDTO.RoleId;
                }
                 user.IsDelete = userDTO.IsDelete;
                if (userDTO.SkillIds != null)
                {
                    foreach (var skillToAdd in userDTO.SkillIds)
                    {
                        if (!user.UserSkills.Any(s => s.SkillId == skillToAdd.SkillId))
                        {
                            await userRepository.AddUserSkill(user.Id, skillToAdd.SkillId, skillToAdd.Experiences);
                        }
                    }

                    var skillsToRemove = user.UserSkills
                        .Where(s => !userDTO.SkillIds.Any(sa => sa.SkillId == s.SkillId))
                        .ToList();

                    foreach (var skill in skillsToRemove)
                    {
                        await userRepository.RemoveSkill(user.Id, skill.SkillId);
                    }
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

        public async Task<UserDTO> CreateUser(UserCreateDTO userDTO)
        {
            var user = mapper.Map<User>(userDTO);

            var createdUser = await userRepository.CreateUser(user);

            return mapper.Map<UserDTO>(createdUser);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
