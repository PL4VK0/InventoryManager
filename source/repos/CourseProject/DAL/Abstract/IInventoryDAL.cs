using DTO;

namespace DAL.Abstract
{
    public interface IInventoryDAL
    {
        List<Inventory> GetAll();
        Inventory GetByID(short id);
        short DeleteByID(short id);
        Inventory Add(Inventory inventory);
        void Update(Inventory inventory);
    }
}
