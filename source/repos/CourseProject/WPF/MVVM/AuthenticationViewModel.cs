using Business_Logic.Abstract;
using DTO;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPF.MVVM
{
    public class AuthenticationViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        IAuthenticationService _authenticationService;
        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            LoginCommand = new RelayCommand(Login, param => string.IsNullOrEmpty(Error));
            _authenticationService = authenticationService;
        }
        private string password = string.Empty;
        private string userName = string.Empty;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName));}
        }
        public string this[string columnName]
        {
            get
            {
                string result = string.Empty;
                if (columnName == nameof(UserName))
                    result = CheckUserName();
                if (columnName == nameof(Password))
                    result = CheckPassword();
                return result;
            }
        }

        public ICommand LoginCommand { get; }

        public string Error
        {
            get { return (CheckUserName() + ' ' + CheckPassword()).Trim(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string CheckUserName()
        {
            if(UserName.Trim().Length<=2)
                return "Well, the userName len must be \\ge 3!";
            return string.Empty;
        }
        private string CheckPassword()
        {
            if (Password.Trim().Length <= 3)
                return "Password len must be \\ge 4!";
            return string.Empty;
        }

        private void Login(object qch)
        {
            if(!LoginCommand.CanExecute(qch)) return;
            if (!_authenticationService.Authentication(UserName, Password))
            {
                MessageBox.Show("WROGN PASSOWRD OR USERNAMEW!","EOERR!",MessageBoxButton.YesNo,MessageBoxImage.Question);
                return;
            }
            MessageBox.Show("YES!");
        }
    }
}
