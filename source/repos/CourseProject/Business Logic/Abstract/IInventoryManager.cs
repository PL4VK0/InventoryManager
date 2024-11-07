using DTO;

namespace Business_Logic.Abstract
{
    public interface IInventoryManager
    {

        List<Ware> GetAllWares();
        Ware GetWareByID(int id);
        Order PlaceOrder(Order order);

        List<Order> GetAllOrders();
        bool DiscardOrderByID(short ID);
        bool ReceiveOrder(Order order);
        public bool UpdateOrder(Order order);
    }
}
