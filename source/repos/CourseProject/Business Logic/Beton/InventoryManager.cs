using Business_Logic.Abstract;
using DAL.Abstract;
using DAL.Beton;
using DTO;
using WPF.ViewModels;

namespace Business_Logic.Beton
{
    public class InventoryManager : IInventoryManager
    {
        private readonly IManagerDAL managerDAL;
        private readonly IOrderDAL orderDAL;
        private readonly IWareDAL wareDAL;
        private readonly IWareInventoryDAL wareInventoryDAL;
        public Manager? CurrentManager { get; set; } = null;
        public InventoryManager(IManagerDAL managerDAL,IOrderDAL orderDAL, IWareDAL wareDAL, IWareInventoryDAL wareInventoryDAL)
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

        public void CommitOrder(short id)
        {
            Order order = orderDAL.GetByID(id);
            WareInventory? inventoryItem = wareInventoryDAL.GetByID(order.WareID);
            if (inventoryItem == null)
                wareInventoryDAL.Add(new WareInventory
                {
                    WareID = order.WareID,
                    Count = order.Count,
                    WareName = wareDAL.GetByID(order.WareID).WareName
                });
            else
                wareInventoryDAL.Update(new WareInventory
                {
                    WareID = order.WareID,
                    Count = Convert.ToInt16(order.Count + inventoryItem.Count),
                    WareName = wareDAL.GetByID(order.WareID).WareName
                });
            orderDAL.DeleteByID(order.OrderID);
        }
    }
}
