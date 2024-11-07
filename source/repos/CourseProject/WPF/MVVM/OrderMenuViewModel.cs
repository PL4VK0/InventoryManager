using Azure.Identity;
using Business_Logic.Beton;
using DTO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.MVVM
{
    public class OrderMenuViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        InventoryManager _inventoryManager;

        public event PropertyChangedEventHandler? PropertyChanged;

        private short count;
        private  List<Ware>? wares;
        public List<Ware>? Wares
        {
            get { return wares; }
            set { wares = value; }
        }
        public short Count
        {
            get { return count; }
            set
            {
                count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
        private Ware? selectedWare;
        public Ware? SelectedWare
        {
            get { return selectedWare; }
            set
            {
                if(value==selectedWare) return;
                OnPropertyChanged(nameof(SelectedWare));
                selectedWare = value;
            }
        }

        private tblOrder? selectedTblOrder;
        public tblOrder? SelectedTblOrder
        {
            get => selectedTblOrder;
            set
            {
                if (selectedTblOrder == value) return;
                if(value == null) return;
                //if (selectedTblOrder == null) return;
                selectedTblOrder = value;
                OnPropertyChanged(nameof(SelectedTblOrder));
                SelectedWare = wares?.Find(w=>w.WareID== selectedTblOrder?.WareID);
                OnPropertyChanged(nameof(SelectedWare));
                Count = selectedTblOrder.Count;
            }
        }
        public ObservableCollection<tblOrder> Orders { get; set; }
        public OrderMenuViewModel(InventoryManager inventroyManager)
        {
            _inventoryManager = inventroyManager;


            //initializing th e tblOrder values
            List<tblOrder> tblOrders = new List<tblOrder>();
            List<Order> orders = _inventoryManager.GetAllOrders();
            List<Manager> managers = _inventoryManager.GetAllManagers();
            wares = _inventoryManager.GetAllWares();
            foreach (var order in orders)
            {
                string managerUN = managers.Find(m => m.ManagerID == order.ManagerID).UserName;
                string wareName = wares.Find(w => w.WareID == order.WareID).WareName;
                tblOrders.Add(new tblOrder
                {
                    ManagerID = order.ManagerID,
                    ManagerUserName = managerUN,
                    WareName = wareName,
                    WareID = order.WareID,
                    OrderID = order.OrderID,
                    Count = order.Count,
                    Date = order.Date
                });
            }
            tblOrders = tblOrders.OrderByDescending(o => o.Date).ToList();
            Orders = new ObservableCollection<tblOrder>(tblOrders);
            //OrderOrdersHAHA();
            //en d of initializqtion


            DiscardSelectedOrder = new RelayCommand(Discard, param => SelectedTblOrder != null);
            UpdateSelectedOrder = new RelayCommand(Update,Predicate); ;
        }
        private bool Predicate(object qch)
        {
            if(SelectedTblOrder==null)
                return false;
            if (SelectedTblOrder.WareID == SelectedWare?.WareID && SelectedTblOrder.Count == Count)
                return false;
            return true;
        }


        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public ICommand DiscardSelectedOrder {  get; }
        public ICommand UpdateSelectedOrder { get; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();



        private void Discard(object qch)
        {
            _inventoryManager.DiscardOrderByID(selectedTblOrder.OrderID);
            Orders.Remove(selectedTblOrder);
        }
        private void Update(object obj)
        {
            //todo: make an update NOW!
            Order toUpdate = new Order
            {
                OrderID = SelectedTblOrder.OrderID,
                WareID = SelectedWare.WareID,
                Count = Count,
                ManagerID = _inventoryManager.CurrentManager.ManagerID,
                Date = DateTime.Now
            };
            //something wrong: this thing doesnt work parce que il n'y a aucun count!(set to null and called when expected not to be null!)'
            _inventoryManager.UpdateOrder(toUpdate);
            SelectedTblOrder.ManagerID = toUpdate.ManagerID;
            SelectedTblOrder.WareID = toUpdate.WareID;
            SelectedTblOrder.Count = toUpdate.Count;
            SelectedTblOrder.ManagerUserName = _inventoryManager.CurrentManager.UserName;

            Orders.Remove(SelectedTblOrder);
            Orders.Add(new tblOrder
            {
                OrderID = toUpdate.OrderID,
                WareID = toUpdate.WareID,
                Count = toUpdate.Count,
                ManagerID = toUpdate.ManagerID,
                Date = toUpdate.Date,
                WareName = SelectedWare.WareName,
                ManagerUserName = _inventoryManager.CurrentManager.UserName
            });
            OnPropertyChanged(nameof(SelectedTblOrder));
            RebiuldAndOrderOrdersHAHA();
        }
        private void RebiuldAndOrderOrdersHAHA()
        {
            var orders = Orders.OrderByDescending(o => o.Date).ToList();
            Orders.Clear();
            foreach (var order in orders)
                Orders.Add(order);
        }
    }
}


//todo: wareName textBox -> wareName listBox, change the count,place an order,commit an order,discard an order
