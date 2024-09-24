using AutoMapper;
using BusinessObjects.DTO;
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
    }
}
