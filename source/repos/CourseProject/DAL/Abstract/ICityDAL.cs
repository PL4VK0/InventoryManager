using DTO;

namespace DAL.Abstract
{
    public interface ICityDAL
    {
        List<City> GetAll();
        City GetByID(short id);
        short DeleteByID(short id);
        City Add(City city);

        void Update(City city);
    }
}
