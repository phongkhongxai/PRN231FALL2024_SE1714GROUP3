﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DTO; 

namespace Services
{
    public interface IUserService
    {
        public Task<UserResponseDTO> GetUserById(long id);
        Task<List<UserResponseDTO>> GetAllUsers();
        Task<UserDTO> CreateUser(UserCreateDTO userDTO);
        Task<UserDTO> UpdateUser(long id, UserUpdateDTO userDTO);
        Task<bool> DeleteUser(long id);
        Task<UserDTO> FindByEmail(string email);
        Task<bool> ChangePassword(long id, ChangePasswordDTO changePasswordDTO);

    }
}
