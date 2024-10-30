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
        private readonly WareInventoryDAL inventoryDAL;
        public InventoryManager(ManagerDAL managerDAL,OrderDAL orderDAL, WareDAL wareDAL, WareInventoryDAL inventoryDAL)
        {
            this.orderDAL = orderDAL;
            this.wareDAL = wareDAL;
            this.inventoryDAL = inventoryDAL;
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
        public void DiscardOrder(Order order)
        {
            orderDAL.DeleteByID(order.OrderID);
        }
        public void ReceiveOrder(Order order)
        {
            
        }
        public void UpdateOrder(Order order)
        {

        }
    }
}
