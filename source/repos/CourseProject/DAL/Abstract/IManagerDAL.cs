using DTO;

namespace DAL.Abstract
{
    public interface IManagerDAL
    {
        List<Manager> GetAll();
        Manager GetByID(short id);
        short DeleteByID(short id);
        Manager Add(Manager manager);
        void Update(Manager manager);
    }
}
