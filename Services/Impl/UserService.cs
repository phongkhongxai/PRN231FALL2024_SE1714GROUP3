using AutoMapper;
using BusinessObjects.DTO;
using DAL.DbContenxt;
using DAL.Repositories;

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

        public async Task<UserDTO> GetUserById(long id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            return mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> GetAllUsers()
        {
            var user = await userRepository.GetAllUsers();
            return mapper.Map<List<UserDTO>>(user);
        }

        public async Task<UserDTO> UpdateUser(long id, UserUpdateDTO userDTO)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                if (userDTO.Email != null)
                {
                    user.Email= userDTO.Email;
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
    }
}
