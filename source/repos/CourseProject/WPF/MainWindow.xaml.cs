using System.Windows;
using WPF.MVVM;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new AuthenticationViewModel();
            DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}