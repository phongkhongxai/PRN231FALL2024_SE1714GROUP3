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

        public async Task UpdateUser(UserDTO userDTO)
        {
            var user = await userRepository.GetUserByIdAsync(userDTO.Id);
            if (user != null)
            {
                mapper.Map(userDTO, user);
                await userRepository.UpdateUser(user);
            }
        }

        public async Task DeleteUser(long id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                //mapper.Map(userDTO, user);
                await userRepository.DeleteUser(id);
            }
        }
    }
}
