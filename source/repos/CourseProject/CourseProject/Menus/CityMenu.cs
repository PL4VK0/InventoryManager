using DAL.Beton;
using DTO;

namespace CourseProject.Menus
{
    public class CityMenu
    {
        private readonly CityDAL cDAL;
        public CityMenu(string connectionString)
        { cDAL = new CityDAL(connectionString); }
        public void Show()
        {
            char input;
            do
            {

                Console.WriteLine("1 - Add new city\n" +
                                  "2 - Show all cities\n" +
                                  "3 - Update city by id\n" +
                                  "4 - Delete city by id\n" +
                                  "0 - Go back");
                input = Convert.ToChar(Console.ReadLine());

                switch (input)
                {
                    case '1':
                        AddCity();
                        break;
                    case '2':
                        ListAllCities();
                        break;
                    case '3':
                        UpdateCityByID();
                        break;
                    case '4':
                        DeleteCityByID();
                        break;
                }
            } while (input != '0');
        }
        void AddCity()
        {
            string cityName;
            Console.WriteLine("Enter cityName: ");
            cityName = Console.ReadLine();

            City city = new City
            {
                CityName = cityName
            };
            City newCity = cDAL.Add(city);
            Console.WriteLine($"Added\n{newCity}");
        }
        void UpdateCityByID()
        {
            ListAllCities();

            short id;

            Console.WriteLine("Enter the id of the ware you want to change: ");

            id = Convert.ToInt16(Console.ReadLine());

            string cityName;

            Console.WriteLine("Enter new cityName: ");
            cityName = Console.ReadLine();

            City updatedCity = new City
            {
                CityName = cityName,
                CityID = id
            };
            cDAL.Update(updatedCity);
            Console.WriteLine($"Updated to\n{updatedCity}");
        }
        void ListAllCities()
        {
            var cities = cDAL.GetAll();
            if (cities.Count == 0)
            {
                Console.WriteLine("There are no cities we supply...");
                return;
            }
            foreach (var city in cities)
            {
                Console.WriteLine(city);
            }
        }
        void DeleteCityByID()
        {
            short id;
            ListAllCities();
            Console.WriteLine("Enter id of the city you want to destroy: ");
            id = Convert.ToInt16(Console.ReadLine());
            short deleted = cDAL.DeleteByID(id);
            if (deleted == 0)
            {
                Console.WriteLine("There were no cities with such id...");
                return;
            }
            Console.WriteLine($"City with {id} id was DESTROYED!");
        }
    }
}
