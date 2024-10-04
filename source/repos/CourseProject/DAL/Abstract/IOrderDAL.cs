using DTO;

namespace DAL.Abstract
{
    public interface IOrderDAL
    {
        List<Order> GetAll();
        Order GetByID(short id);
        short DeleteByID(short id);
        Order Add(Order order);
        void Update(Order order);
    }
}
