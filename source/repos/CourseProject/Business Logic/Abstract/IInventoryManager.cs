using DTO;

namespace Business_Logic.Abstract
{
    public interface IInventoryManager
    {
        List<Ware> GetAllWares();
        Ware GetWareByID(int id);
        Order PlaceOrder(Order order);

        List<Order> GetAllOrders();
        void DiscardOrder(Order order);
        void ReceiveOrder(Order order);
        public void UpdateOrder(Order order);
    }
}
