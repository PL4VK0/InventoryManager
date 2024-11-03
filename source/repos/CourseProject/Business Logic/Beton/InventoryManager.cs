using Business_Logic.Abstract;
using DAL.Beton;
using DTO;

namespace Business_Logic.Beton
{
    public class InventoryManager : IInventoryManager
    {
        private readonly ManagerDAL managerDAL;
        private readonly OrderDAL orderDAL;
        private readonly WareDAL wareDAL;
        private readonly WareInventoryDAL wareInventoryDAL;
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
        public Ware GetWareByID(int id)
        {
            return wareDAL.GetByID((short)id);
        }
        public Order PlaceOrder(Order order)
        {
            return orderDAL.Add(order);
        }

        public List<Order> GetAllOrders()
        {
            return orderDAL.GetAll();
        }
        public bool DiscardOrder(Order order)
        {
            try
            {
                orderDAL.DeleteByID(order.OrderID);
                return true;
            }catch
            {
                return false;
            }
        }
        public bool ReceiveOrder(Order order)
        {
            WareInventory itemToUpdate = new WareInventory
            {
                WareID = order.WareID,
                Count = order.Count
            };
            try
            {
                wareInventoryDAL.Update(itemToUpdate);
                DiscardOrder(order);
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
    }
}
