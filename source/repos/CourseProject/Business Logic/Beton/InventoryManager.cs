using Business_Logic.Abstract;
using DAL.Beton;
using DTO;
using WPF.ViewModels;

namespace Business_Logic.Beton
{
    public class InventoryManager : IInventoryManager
    {
        private readonly ManagerDAL managerDAL;
        private readonly OrderDAL orderDAL;
        private readonly WareDAL wareDAL;
        private readonly WareInventoryDAL wareInventoryDAL;
        public Manager? CurrentManager { get; set; } = null;
        public InventoryManager(ManagerDAL managerDAL,OrderDAL orderDAL, WareDAL wareDAL, WareInventoryDAL wareInventoryDAL)
        {
            this.orderDAL = orderDAL;
            this.wareDAL = wareDAL;
            this.wareInventoryDAL = wareInventoryDAL;
            this.managerDAL= managerDAL;
        }
        public List<Ware> GetAllWares()
        {
            return wareDAL.GetAll();
        }
        public Order PlaceOrder(Order order)
        {
            return orderDAL.Add(order);
        }

        public List<Order> GetAllOrders()
        {
            return orderDAL.GetAll();
        }
        public bool DiscardOrderByID(short ID)
        {
            try
            {
                orderDAL.DeleteByID(ID);
                return true;
            }catch
            {
                return false;
            }
        }
        public bool UpdateOrder(Order order)
        {
            try
            {
                orderDAL.Update(order);
                return true;
            }catch
            {
                return false;
            }
        }

        public List<WareInventory> GetAllWareInventory()
        {
            return wareInventoryDAL.GetAll();
        }
        public List<Manager> GetAllManagers()
        {
            return managerDAL.GetAll();
        }

        public void CommitOrder(tblOrder tblOrder)
        {
            WareInventory inventoryItem = wareInventoryDAL.GetByID(tblOrder.WareID);
            if (inventoryItem == null)
                wareInventoryDAL.Add(new WareInventory
                {
                    WareID = tblOrder.WareID,
                    Count = tblOrder.Count,
                    WareName = tblOrder.WareName
                });
            else
                wareInventoryDAL.Update(new WareInventory
                {
                    WareID = tblOrder.WareID,
                    Count = Convert.ToInt16(tblOrder.Count + inventoryItem.Count),
                    WareName = tblOrder.WareName
                });
            orderDAL.DeleteByID(tblOrder.OrderID);
        }
    }
}
