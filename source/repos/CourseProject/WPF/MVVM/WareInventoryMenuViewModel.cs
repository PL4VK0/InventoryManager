using Business_Logic.Beton;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM
{
    public class WareInventoryMenuViewModel
    {
        public ObservableCollection<Ware>? Wares { get; set; }
        public ObservableCollection<WareInventory> WareInventory { get; set; }

        private InventoryManager _inventoryManager;
        
        
        public WareInventoryMenuViewModel(InventoryManager inventoryManager)
        {
            _inventoryManager = inventoryManager;
            Wares = new ObservableCollection<Ware>(_inventoryManager.GetAllWares());
            WareInventory = new ObservableCollection<WareInventory>(_inventoryManager.GetAllWareInventory());
        }

    }
}
