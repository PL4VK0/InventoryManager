using Business_Logic.Beton;
using System.Windows;
using WPF.MVVM;

namespace WPF
{
    /// <summary>
    /// Interaction logic for OrderMenu.xaml
    /// </summary>
    public partial class OrderMenu : Window
    {
        public OrderMenu(InventoryManager inventoryManager)
        {
            InitializeComponent();
            var viewModel = new OrderMenuViewModel(inventoryManager);
            DataContext = viewModel;
        }
    }
}
