using DTO;

namespace DAL.Abstract
{
    public interface IWareDAL
    {
        List<Ware> GetAll();
        Ware GetByID(short id);
        short DeleteByID(short id);
        Ware Add(Ware ware);
        void Update(Ware ware);
    }
}
