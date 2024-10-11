using DAL.Beton;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Menus
{
    public class WareMenu
    {
        private readonly WareDAL wDAL;
        public WareMenu(string connectionString)
        { wDAL = new WareDAL(connectionString); }
        public void Show()
        {
            char input;
            do
            {

                Console.WriteLine("1 - Add new ware\n" +
                                  "2 - Show all wares\n" +
                                  "3 - Update ware by id\n" +
                                  "4 - Delete ware by id\n" +
                                  "0 - Go back");
                input = Convert.ToChar(Console.ReadLine());

                switch (input)
                {
                    case '1':
                        AddWare();
                        break;
                    case '2':
                        ListAllWares();
                        break;
                    case '3':
                        UpdateWareByID();
                        break;
                    case '4':
                        DeleteWareByID();
                        break;
                }
            } while (input != '0');
        }

        void UpdateWareByID()
        {
            ListAllWares();

            short id;

            Console.WriteLine("Enter the id of the ware you want to change: ");

            id = Convert.ToInt16(Console.ReadLine());

            string wareName;
            string wareDescription;
            float cost;

            Console.WriteLine("Enter new wareName: ");
            wareName = Console.ReadLine();

            Console.WriteLine("Enter new wareDescription: ");
            wareDescription = Console.ReadLine();

            Console.WriteLine("Enter new wareCost (if that wasn't expensive enough): ");
            cost = (float)Convert.ToDouble(Console.ReadLine());

            Ware updatedWare = new Ware
            {
                WareID = id,
                WareName = wareName,
                WareDescription = wareDescription,
                Cost = cost,
            };
            wDAL.Update(updatedWare);
            Console.WriteLine($"Updated to\n{updatedWare}");
        }

        void DeleteWareByID()
        {
            short id;
            ListAllWares();
            Console.WriteLine("Enter id of the ware you want gone: ");
            id = Convert.ToInt16(Console.ReadLine());
            short deleted = wDAL.DeleteByID(id);
            if (deleted == 0)
            {
                Console.WriteLine("There were no wares with such id...");
                return;
            }
            Console.WriteLine($"ware with {id} id was deleted!");
        }

        void ListAllWares()
        {
            var wares = wDAL.GetAll();
            if (wares.Count == 0)
            {
                Console.WriteLine("There are no wares...");
                return;
            }
            foreach (var ware in wares)
            {
                Console.WriteLine(ware);
            }
        }

        void AddWare()
        {
            string wareName;
            string wareDescription;
            float wareCost;

            Console.WriteLine("Enter wareName: ");
            wareName = Console.ReadLine();
            Console.WriteLine("Enter wareDescription: ");
            wareDescription = Console.ReadLine();
            Console.WriteLine("Enter ware price: ");
            wareCost = (float)Convert.ToDouble(Console.ReadLine());

            Ware ware = new Ware
            {
                WareName = wareName,
                WareDescription = wareDescription,
                Cost = wareCost
            };
            Ware newWare = wDAL.Add(ware);

            Console.WriteLine($"Added\n{newWare}");
        }
    }
}
