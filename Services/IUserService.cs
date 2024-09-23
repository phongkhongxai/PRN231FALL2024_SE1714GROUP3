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
        public UserDTO GetUserById(long id); 
    }
}
