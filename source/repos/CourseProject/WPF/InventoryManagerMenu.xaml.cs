using Business_Logic.Abstract;
using Business_Logic.Beton;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.MVVM;

namespace WPF
{
    /// <summary>
    /// Interaction logic for InventoryManagerMenu.xaml
    /// </summary>
    public partial class InventoryManagerMenu : Window
    {
        InventoryManager _invManager;
        public InventoryManagerMenu(InventoryManager invManager)
        {
            InitializeComponent();
            var viewModel = new InventoryManagerMenuViewModel(invManager,OpenInvMenu,OpenOrderMenu);
            _invManager = invManager;
            DataContext = viewModel;
        }
        private void OpenInvMenu(object qch)
        {
            var invMenu = new WareInventoryMenu(_invManager);
            invMenu.Show();
        }
        private void OpenOrderMenu(object qch)
        {
            var orderMenu = new OrderMenu(_invManager);
            orderMenu.Show();
        }
    }
}
