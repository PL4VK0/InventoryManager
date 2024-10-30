using DTO;
using System.Globalization;
using System.Windows.Controls;

namespace WPF.Validation
{
    public class ManagerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Manager manager = (Manager)value;
            if(manager.UserName.Length<=0)
            {
                return new ValidationResult(false,null);
            }
            if(manager.Password.Length<=0)
            {
                return new ValidationResult(false,null);
            }
            return new ValidationResult(true,null);
        }
    }
}
