using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Abstract
{
    public interface IAuthenticationService
    {
        bool Authentication(string username, string password);
    }
}
