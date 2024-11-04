using Business_Logic.Abstract;
using DAL.Beton;
using DTO;
using Services;

namespace Business_Logic.Beton
{
    public class AuthenticationService:IAuthenticationService
    {
        private ManagerDAL managerDAL;
        public AuthenticationService(ManagerDAL managerDAL)
        {
            this.managerDAL = managerDAL;
        }
        public Manager? Authentication(string UserName,string Password)
        {
            List<Manager> managers = managerDAL.GetAll();
            Manager? toAuthenticate = managers.Find(m => m.UserName == UserName);
            if (toAuthenticate == null)
                return null;
            string saltedPassword = Password+managerDAL.GetSalt(toAuthenticate);
            string hashedPasswordToCheck = HashOperations.GetHashString(saltedPassword);
            string actualPassword = toAuthenticate.Password;
            
            if(hashedPasswordToCheck != actualPassword) 
                return null;
            return toAuthenticate;
        }
    }
}
