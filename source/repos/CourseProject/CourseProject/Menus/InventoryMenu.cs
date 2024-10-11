using DAL.Beton;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Menus
{
    public class InventoryMenu
    {
        private readonly InventoryDAL iDAL;
        public InventoryMenu(string connectionString)
        { iDAL = new InventoryDAL(connectionString); }
        public void Show()
        {
            char input;
            do
            {

                Console.WriteLine("1 - Add new inventory\n" +
                                  "2 - Show all inventories\n" +
                                  "3 - Update inventory by id\n" +
                                  "4 - Delete inventory by id\n" +
                                  "0 - Go back");
                input = Convert.ToChar(Console.ReadLine());

                switch (input)
                {
                    case '1':
                        AddInventory();
                        break;
                    case '2':
                        ListAllInventories();
                        break;
                    case '3':
                        UpdateInventoryByID();
                        break;
                    case '4':
                        DeleteInventoryByID();
                        break;
                }
            } while (input != '0');
        }
        void UpdateInventoryByID()
        {
            ListAllInventories();

            short id;

            Console.WriteLine("Enter the id of the inventory you want to change: ");

            id = Convert.ToInt16(Console.ReadLine());

            short cityID;

            Console.WriteLine("Enter new cityID: ");
            cityID = Convert.ToInt16(Console.ReadLine());

            Inventory updatedInventory = new Inventory
            {
                CityID = cityID,
                InventoryID = id
            };
            iDAL.Update(updatedInventory);
            Console.WriteLine($"Updated to\n{updatedInventory}");
        }

        void DeleteInventoryByID()
        {
            ListAllInventories();

            short id;
            Console.WriteLine("Enter the id of inventory you want to destroy: ");
            id = Convert.ToInt16(Console.ReadLine());

            short deleted = iDAL.DeleteByID(id);
            if (deleted == 0)
            {
                Console.WriteLine("No inventory was found with such id...");
                return;
            }
            Console.WriteLine($"Deleted inventory with id {id}...");
        }

        void ListAllInventories()
        {
            var inventories = iDAL.GetAll();

            if (inventories.Count == 0)
            {
                Console.WriteLine("There are no inventories...");
                return;
            }
            foreach (var inventory in inventories)
            {
                Console.WriteLine(inventory);
            }

        }
        void AddInventory()
        {
            short cityID;

            Console.WriteLine("Enter id of the city: ");
            cityID = Convert.ToInt16(Console.ReadLine());

            var inventory = new Inventory
            { CityID = cityID };
            var newInventory = iDAL.Add(inventory);
            Console.WriteLine($"Added\n{newInventory}");
        }

    }
}
