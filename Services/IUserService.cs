using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DTO; 

namespace Services
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserById(long id);
        Task<List<UserDTO>> GetAllUsers();
        Task<UserDTO> UpdateUser(long id, UserUpdateDTO userDTO);
        Task<bool> DeleteUser(long id);
        Task<UserDTO> FindByEmail(string email);
        Task<bool> ChangePassword(long id, ChangePasswordDTO changePasswordDTO);

    }
}
