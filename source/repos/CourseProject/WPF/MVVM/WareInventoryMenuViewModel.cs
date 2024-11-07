using Business_Logic.Beton;
using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF.MVVM
{
    public class WareInventoryMenuViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Ware>? Wares { get; set; }
        public ObservableCollection<WareInventory> WareInventory { get; set; }

        private InventoryManager _inventoryManager;
        Action<string, string> DisplayMessageBox;
        
        
        public WareInventoryMenuViewModel(InventoryManager inventoryManager,Action<string,string> displayMessageBox)
        {
            _inventoryManager = inventoryManager;
            Wares = new ObservableCollection<Ware>(_inventoryManager.GetAllWares());
            WareInventory = new ObservableCollection<WareInventory>(_inventoryManager.GetAllWareInventory());
            RefreshInventoryCommand = new RelayCommand(RefreshInventoryItems);
            SortWaresByCostCommand = new RelayCommand(SortWaresByCost,SortWaresPredicate);
            SortWaresByNameCommand  = new RelayCommand(SortWaresByName,SortWaresPredicate);
            FindWaresByNameCommand = new RelayCommand(FindWares);
            FindInventoryItemByNameCommand = new RelayCommand(FindItems);
            SortInventoryItemByNameCommand = new RelayCommand(SortItemsByName,SortItemsPredicate);

            DisplayMessageBox = displayMessageBox;
        }

        private bool SortItemsPredicate(object obj)
        {
            return WareInventory != null;
        }

        private void SortItemsByName(object obj)
        {
            var items = WareInventory.OrderBy(i=> i.WareName).ToList();
            WareInventory.Clear();
            if (descendingForInventoryItems)
                items.Reverse();
            foreach (var item in items)
                WareInventory.Add(item);
        }

        private void FindItems(object obj)
        {
            var items = _inventoryManager.GetAllWareInventory();
            if (!string.IsNullOrEmpty(TxtBoxInventoryItemName))
                items = items.FindAll(i=>i.WareName.ToLower().Contains(TxtBoxInventoryItemName.ToLower())).ToList();
            WareInventory.Clear();
            foreach (var item in items)
                WareInventory.Add(item);
        }

        private void FindWares(object obj)
        {
            var wares = _inventoryManager.GetAllWares();
            if(!string.IsNullOrEmpty(TxtBoxWareName))
                wares = wares.FindAll(w=>w.WareName.ToLower().Contains(TxtBoxWareName.ToLower())).ToList();
            Wares.Clear();
            foreach (var ware in wares)
                Wares.Add(ware);
        }

        private bool descendingForWares;
        private bool descendingForInventoryItems;
        public bool DescendingForInventoryItems
        {
            get { return descendingForInventoryItems; }
            set { descendingForInventoryItems = value;}
        }
        public bool DescendingForWares
        {
            get {  return descendingForWares; }
            set { descendingForWares = value; }
        }
        private string txtBoxWareName;
        private string txtBoxInventoryItemName;
        public string TxtBoxInventoryItemName
        {
            get { return txtBoxInventoryItemName; }
            set { txtBoxInventoryItemName = value; OnPropertyChanged(nameof(TxtBoxInventoryItemName)); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public string TxtBoxWareName
        {
            get { return txtBoxWareName; }
            set { txtBoxWareName = value; OnPropertyChanged(nameof(TxtBoxWareName)); }
        }

        private void SortWaresByName(object obj)
        {
            var wares = Wares.OrderBy(w=>w.WareName).ToList();
            Wares.Clear();
            if(descendingForWares)
                wares.Reverse();
            foreach (var ware in wares)
                Wares.Add(ware);
        }

        private bool SortWaresPredicate(object obj)
        {
            return Wares != null;
        }

        private void SortWaresByCost(object obj)
        {
            var wares = Wares.OrderBy(w => w.Cost).ToList();
            Wares.Clear();
            if (descendingForWares)
                wares.Reverse();
            foreach (var ware in wares)
                Wares.Add(ware);
        }

        private void RefreshInventoryItems(object obj)
        {
            var invItems = _inventoryManager.GetAllWareInventory();
            WareInventory.Clear();
            foreach (var item in invItems)
                WareInventory.Add(item);
            DisplayMessageBox?.Invoke("ITEMS REFRESHED", "DID ANYTHING CHANGE?");
        }

        public ICommand RefreshInventoryCommand { get; }
        public ICommand SortWaresByCostCommand { get; }
        public ICommand SortWaresByNameCommand { get; }
        public ICommand FindWaresByNameCommand { get; }
        public ICommand FindInventoryItemByNameCommand { get; }
        public ICommand SortInventoryItemByNameCommand { get; }
    }
}
