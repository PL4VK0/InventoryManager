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

        private InventoryManager _inventoryManager;
        private short count = 0;
        private  List<Ware>? wares;
        private Action<string, string> DisplayMessageBox;

        public event PropertyChangedEventHandler? PropertyChanged;

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
                UpdateCommandStates();
            }
        }
        private string countString;
        public string CountString
        {
            get { return countString; }
            set 
            { 
                countString = value; OnPropertyChanged(nameof(CountString));
                try {  Count = Convert.ToInt16(value); } catch {  Count= -1; }
            }
        }
        private Ware? selectedWare;
        public Ware? SelectedWare
        {
            get { return selectedWare; }
            set
            {
                if(value==selectedWare) return;
                selectedWare = value;
                OnPropertyChanged(nameof(SelectedWare));
                UpdateCommandStates();
            }
        }

        private tblOrder? selectedTblOrder;
        public tblOrder? SelectedTblOrder
        {
            get { return selectedTblOrder; }
            set
            {
                if (selectedTblOrder == value) return;
                selectedTblOrder = value;
                if(selectedTblOrder==null) return;
                SelectedWare = wares?.Find(w=>w.WareID== selectedTblOrder?.WareID);
                CountString = selectedTblOrder.Count.ToString();
                OnPropertyChanged(nameof(SelectedWare));
                UpdateCommandStates();
            }
        }
        public ObservableCollection<tblOrder> Orders { get; set; }
        public OrderMenuViewModel(InventoryManager inventroyManager,Action<string,string> displayMessageBox)
        {
            _inventoryManager = inventroyManager;
            DisplayMessageBox = displayMessageBox;

            //initializing th e tblOrder values
            //oops, i copypasted it into another funtion!
            Orders = new ObservableCollection<tblOrder>(GetAllTblOrders());
            //en d of initializqtion


            DiscardSelectedOrder = new RelayCommand(Discard,CommitAndDiscardPredicate);
            UpdateSelectedOrder = new RelayCommand(Update,UpdatePredicate);
            CommitSelectedOrder = new RelayCommand(Commit,CommitAndDiscardPredicate);
            PlaceNewOrderCommand = new RelayCommand(Place, PlaceOrderPredicate);
        }
        private List<tblOrder> GetAllTblOrders()
        {
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
            return tblOrders;
        }

        private void Place(object obj)
        {
            var newOrder = new Order
            {
                Count = Count,
                WareID = SelectedWare.WareID,
                ManagerID = _inventoryManager.CurrentManager.ManagerID,
                Date = DateTime.Now,
            };
            _inventoryManager.PlaceOrder(newOrder);
            RebiuldAndOrderOrdersHAHA();
            DisplayMessageBox.Invoke("NEW ORDER!!!", "SUCCESSFUL ORDER PLACEMENT!");
        }

        private void Commit(object obj)
        {
            _inventoryManager.CommitOrder(SelectedTblOrder);
            Orders.Remove(SelectedTblOrder);
            SelectedTblOrder = null;
            OnPropertyChanged(nameof(SelectedTblOrder));
            UpdateCommandStates();
            DisplayMessageBox.Invoke("This order has been commited. Check the inventory!", "Successful commit!");
        }

        private bool UpdatePredicate(object qch)
        {
            if (!PlaceOrderPredicate(qch)) 
                return false;
            if(SelectedTblOrder==null) 
                return false;
            if (SelectedTblOrder.WareID == SelectedWare?.WareID && 
                SelectedTblOrder.Count == Count) 
                return false;
            return true;
        }
        private bool PlaceOrderPredicate(object qch)
        {
            if(SelectedWare==null) return false;
            if(Count==null) return false;
            if (Count <= 0 || Count >= 100) return false;
            return true;
        }
        private bool CommitAndDiscardPredicate(object qch)
        {
            if(SelectedTblOrder==null)
                return false;
            return true;
        }


        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public ICommand DiscardSelectedOrder {  get; }
        public ICommand UpdateSelectedOrder { get; }
        public ICommand CommitSelectedOrder { get; }
        public ICommand PlaceNewOrderCommand { get; }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();



        private void Discard(object qch)
        {
            _inventoryManager.DiscardOrderByID(selectedTblOrder.OrderID);
            Orders.Remove(selectedTblOrder);
            SelectedTblOrder = null;
            OnPropertyChanged(nameof(SelectedTblOrder));
            UpdateCommandStates();
            DisplayMessageBox.Invoke("This order has been discarded", "Successful deletion!");
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
            //NOW IT WORKS!
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
            SelectedTblOrder = null;
            OnPropertyChanged(nameof(SelectedTblOrder));
            UpdateCommandStates();
            RebiuldAndOrderOrdersHAHA();
            DisplayMessageBox.Invoke("This order has been updated!", "Successful update!");
        }
        private void RebiuldAndOrderOrdersHAHA()
        {
            var orders = GetAllTblOrders();
            Orders.Clear();
            foreach (var order in orders)
                Orders.Add(order);
        }
        private void UpdateCommandStates()
        {
            (DiscardSelectedOrder as RelayCommand).UpdateCanExecuteChanged();
            (CommitSelectedOrder as RelayCommand).UpdateCanExecuteChanged();
            (UpdateSelectedOrder as RelayCommand).UpdateCanExecuteChanged();
            (PlaceNewOrderCommand as RelayCommand).UpdateCanExecuteChanged();
        }
    }
}


//todo: wareName textBox -> wareName listBox, change the count,place an order,commit an order,discard an order
