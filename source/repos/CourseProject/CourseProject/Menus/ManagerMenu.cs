using DAL.Beton;
using DTO;

namespace CourseProject.Menus
{
    public class ManagerMenu
    {
        private readonly ManagerDAL mDAL;
        public ManagerMenu(string connectionString)
        { mDAL = new ManagerDAL(connectionString); }
        public void Show()
        {
            char input;
            do
            {

                Console.WriteLine("1 - Add new manager\n" +
                                  "2 - Show all managers\n" +
                                  "3 - Update manager by id\n" +
                                  "4 - Delete manager by id\n" +
                                  "0 - Go back");
                input = Convert.ToChar(Console.ReadLine());

                switch (input)
                {
                    case '1':
                        AddManager();
                        break;
                    case '2':
                        ListAllManagers();
                        break;
                    case '3':
                        UpdateManagerByID();
                        break;
                    case '4':
                        DeleteManagerByID();
                        break;
                }
            } while (input != '0');
        }

        void UpdateManagerByID()
        {
            ListAllManagers();

            short id;
            Console.WriteLine("Enter id of the manager you want to change: ");
            id = Convert.ToInt16(Console.ReadLine());
            string firstName;
            string lastName;
            string userName;
            string password;
            short inventoryID;


            Console.WriteLine("Enter newFirstName: ");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter newLastName: ");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter newUserName: ");
            userName = Console.ReadLine();
            Console.WriteLine("Enter newPassword: ");
            password = Console.ReadLine();
            Console.WriteLine("Enter newInventoryID: ");

            Manager manager = new Manager
            {
                ManagerID = id,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password,
            };
            mDAL.Update(manager);

            Console.WriteLine($"Updated to\n{manager}");
        }

        void DeleteManagerByID()
        {
            ListAllManagers();

            short id;
            Console.WriteLine("Enter the id of manager you want gone: ");
            id = Convert.ToInt16(Console.ReadLine());

            short deleted = mDAL.DeleteByID(id);
            if (deleted == 0)
            {
                Console.WriteLine("No manager was found with such id...");
                return;
            }
            Console.WriteLine($"Deleted manager with id {id}. Goodbye...");
        }

        void ListAllManagers()
        {
            var managers = mDAL.GetAll();

            if (managers.Count == 0)
            {
                Console.WriteLine("There are no managers... (nobody wants to work for us)");
                return;
            }
            foreach (var manager in managers)
            {
                Console.WriteLine(manager);
            }
        }

        void AddManager()
        {
            string firstName;
            string lastName;
            string userName;
            string password;


            Console.WriteLine("Enter firstName: ");
            firstName = Console.ReadLine();
            Console.WriteLine("Enter lastName: ");
            lastName = Console.ReadLine();
            Console.WriteLine("Enter userName: ");
            userName = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            password = Console.ReadLine();
            Console.WriteLine("Enter inventoryID: ");

            Manager manager = new Manager
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName,
                Password = password,
                Salt=DateTime.Now.AddSeconds(-DateTime.Now.Second)
            };
            Console.WriteLine("The salt for this manager is "+ manager.Salt.ToString());
            Manager newManager = mDAL.Add(manager);
            Console.WriteLine("The hashpassword for this manager is " + newManager.Password);

            Console.WriteLine($"Added\n{newManager}");
        }
    }
}
