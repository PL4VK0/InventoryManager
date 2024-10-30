using System.ComponentModel;
using System.Windows.Input;

namespace WPF.MVVM
{
    public class AuthenticationViewModel:INotifyPropertyChanged
    {
        private string userName;
        private string password;


        public string UserName
        {
            get { return userName; } 
            set { userName = value;OnPropertyChanged(nameof(UserName));}
        }
        public string Password
        {
            get { return password; }
            set { password=value; OnPropertyChanged(nameof(Password));}
        }

        public ICommand LoginCommand { get; }

        public AuthenticationViewModel()
        {
            LoginCommand = new RelayCommand(o =>
            {
                
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)=>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
