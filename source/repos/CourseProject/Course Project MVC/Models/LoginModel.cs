using System.ComponentModel.DataAnnotations;

namespace Course_Project_MVC.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is mandatory!")]
        [StringLength(20,MinimumLength = 3, ErrorMessage ="UserName should be between 3 and 20 symbols!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Passsssssword is mandatory!")]
        [StringLength(50,MinimumLength = 4, ErrorMessage = "Passsssssword ssssshould be at leasssst 4 sssymbolsss long!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public override bool Equals(object? obj)
        {
            LoginModel? other = obj as LoginModel;
            if (other == null) return false;
            return other.UserName==UserName && other.Password==Password;
        }
    }
}
