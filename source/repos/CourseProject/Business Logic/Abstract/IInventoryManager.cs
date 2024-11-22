using DTO;
using WPF.ViewModels;

namespace Business_Logic.Abstract
{
    public interface IInventoryManager
    {

        List<Ware> GetAllWares();
        Order PlaceOrder(Order order);

        List<Order> GetAllOrders();
        bool DiscardOrderByID(short ID);
        bool UpdateOrder(Order order);
        List<WareInventory> GetAllWareInventory();
        List<Manager> GetAllManagers();
        void CommitOrder(short id);
    }
}
