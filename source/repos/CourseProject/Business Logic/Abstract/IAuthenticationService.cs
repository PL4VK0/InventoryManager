﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Abstract
{
    public interface MyIAuthenticationService
    {
        Manager? Authentication(string username, string password);
    }
}
