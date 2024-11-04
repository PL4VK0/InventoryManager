using Business_Logic.Beton;
using System.Windows.Input;

namespace WPF.MVVM
{
    public class InventoryManagerMenuViewModel
    {
        InventoryManager _inventoryManager;
        Action<InventoryManager> _openInvMenu;
        Action<InventoryManager> _openOrderMenu;
        public InventoryManagerMenuViewModel(InventoryManager inventoryManager,Action<InventoryManager> openInvMenu,Action<InventoryManager> openOrderMenu)
        {
            _inventoryManager = inventoryManager;
            InventoryMenuCommand = new RelayCommand(ShowInventoryMenu);
            OrderMenuCommand = new RelayCommand(ShowOrderMenu);
            _openInvMenu = openInvMenu;
            _openOrderMenu = openOrderMenu;
        }
        public ICommand InventoryMenuCommand { get; }
        public ICommand OrderMenuCommand { get; } 

        void ShowInventoryMenu(object qch)
        {
            _openInvMenu?.Invoke(_inventoryManager);
        }
        void ShowOrderMenu(object qch)
        {
            _openOrderMenu?.Invoke(_inventoryManager);
        }
    }
}
