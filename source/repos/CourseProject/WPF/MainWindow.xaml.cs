using Business_Logic.Abstract;
using Business_Logic.Beton;
using DTO;
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
        InventoryManager inventoryManager;
        MyIAuthenticationService authenticationService;
        public MainWindow(InventoryManager inventoryManager,MyIAuthenticationService authenticationService)
        {
            this.inventoryManager = inventoryManager;
            this.authenticationService = authenticationService;
            InitializeComponent();
            var viewModel = new AuthenticationViewModel(authenticationService,inventoryManager, OpenInvManMenu,ClearPasswordBox);
            DataContext = viewModel;
        }

        private void txtBoxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthenticationViewModel viewModel)
                viewModel.Password = txtBoxPassword.Password;
        }
        private void OpenInvManMenu(InventoryManager invManager)
        {
            var invManMenu = new InventoryManagerMenu(invManager);
            ClearPasswordBox();
            invManMenu.ShowDialog();
        }
        private void ClearPasswordBox()
        {
            txtBoxPassword.Clear();
        }
    }
}