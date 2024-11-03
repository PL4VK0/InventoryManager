using Business_Logic.Abstract;
using System.Windows;
using System.Windows.Controls;
using WPF.MVVM;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IInventoryManager inventoryManager;
        IAuthenticationService authenticationService;
        public MainWindow(IInventoryManager inventoryManager,IAuthenticationService authenticationService)
        {
            this.inventoryManager = inventoryManager;
            this.authenticationService = authenticationService;
            InitializeComponent();
            var viewModel = new AuthenticationViewModel(authenticationService);
            DataContext = viewModel;
        }

        private void txtBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthenticationViewModel viewModel)
                viewModel.Password = txtBoxPassword.Password;
        }
    }
}