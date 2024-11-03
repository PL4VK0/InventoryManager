using DAL.Beton;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Menus
{
    public class OrderMenu
    {
        private readonly OrderDAL ordDAL;
        public OrderMenu(string connectionString)
        { ordDAL = new OrderDAL(connectionString); }
        public void Show()
        {
            char input;
            do
            {

                Console.WriteLine("1 - Add new order\n" +
                                  "2 - Show all orders\n" +
                                  "3 - Update order by id\n" +
                                  "4 - Delete order by id");
                input = Convert.ToChar(Console.ReadLine());

                switch (input)
                {
                    case '1':
                        AddOrder();
                        break;
                    case '2':
                        ListAllOrders();
                        break;
                    case '3':
                        UpdateOrderByID();
                        break;
                    case '4':
                        DeleteOrderByID();
                        break;
                }
            } while (input != '0');
        }
        void UpdateOrderByID()
        {
            ListAllOrders();

            short id;

            Console.WriteLine("Enter orderID you want to change: ");

            id = Convert.ToInt16(Console.ReadLine());

            short managerID;
            short inventoryID;

            Console.WriteLine("Enter new managerID: ");
            managerID = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Enter new inventoryID: ");
            inventoryID = Convert.ToInt16(Console.ReadLine());

            Order updatedOrder = new Order
            {
                OrderID = id,
                ManagerID = managerID,
            };
            ordDAL.Update(updatedOrder);
            Console.WriteLine($"Updated to\n{updatedOrder}");
        }

        void DeleteOrderByID()
        {
            ListAllOrders();

            short id;
            Console.WriteLine("Enter orderID you want gone: ");
            id = Convert.ToInt16(Console.ReadLine());

            short deleted = ordDAL.DeleteByID(id);
            if (deleted == 0)
            {
                Console.WriteLine("No order was found with such id...");
                return;
            }
            Console.WriteLine($"Deleted order with id {id}. Goodbye...");
        }

        void ListAllOrders()
        {
            var orders = ordDAL.GetAll();

            if (orders.Count == 0)
            {
                Console.WriteLine("There are no orders...");
                return;
            }
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }

        void AddOrder()
        {
            short managerID;
            short inventoryID;

            Console.WriteLine("Enter managerID that is making the order: ");
            managerID = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Enter invenotryID where the order is going: ");
            inventoryID = Convert.ToInt16(Console.ReadLine());

            var order = new Order
            {
                ManagerID = managerID,
            };
            var newOrder = ordDAL.Add(order);
            Console.WriteLine($"Added\n{newOrder}");
        }
    }
}
