using Business_Logic.Beton;
using System.Windows;
using WPF.MVVM;

namespace WPF
{
    /// <summary>
    /// Interaction logic for WareInventoryMenu.xaml
    /// </summary>
    public partial class WareInventoryMenu : Window
    {
        public WareInventoryMenu(InventoryManager inventoryManager)
        {
            InitializeComponent();
            var viewModel = new WareInventoryMenuViewModel(inventoryManager,DisplayMessageBox);
            DataContext = viewModel;
        }
        private void DisplayMessageBox(string message,string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation);
        }
    }
}
