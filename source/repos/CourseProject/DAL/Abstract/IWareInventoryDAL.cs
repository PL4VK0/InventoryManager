using DTO;

namespace DAL.Abstract
{
    public interface IWareInventoryDAL
    {
        List<WareInventory> GetAll();
        WareInventory Add(WareInventory wareInventory);
    }
}
