﻿using BusinessObjects.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IAuthRepository
    {
        User GetUserByEmailOrUsername(string emailOrUsername);

    }
}
