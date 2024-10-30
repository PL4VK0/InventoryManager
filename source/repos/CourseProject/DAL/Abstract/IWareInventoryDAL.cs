using DTO;

namespace DAL.Abstract
{
    public interface IWareInventoryDAL
    {
        List<WareInventory> GetAll();
        WareInventory Add(WareInventory wareInventory);
        short DeleteByID(short id);
        void Update(WareInventory wareInventory);
        WareInventory GetByID(short id);
    }
}
