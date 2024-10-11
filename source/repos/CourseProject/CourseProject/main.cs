using CourseProject.Menus;
using DAL.Beton;
using DTO;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();


string connectionString = config.GetConnectionString("InventoryManager");
CityMenu cityMenu = new CityMenu(connectionString);
InventoryMenu invMenu = new InventoryMenu(connectionString);
OrderMenu ordMenu = new OrderMenu(connectionString);
ManagerMenu managerMenu = new ManagerMenu(connectionString);
WareMenu wareMenu = new WareMenu(connectionString);

char input;
Console.WriteLine("Welcome... (nothing more)");
do
{
    Console.WriteLine("1 - Ware operation\n" +
                      "2 - Manager operation\n" +
                      "3 - Order operation\n" +
                      "4 - Inventory operation\n" +
                      "5 - City operation\n" +
                      "0 - Exit our company...");
    Console.WriteLine("Enter the option: ");
    input = Convert.ToChar(Console.ReadLine());


    switch (input)
    {
        case '1':
            wareMenu.Show();
            break;
        case '2':
            managerMenu.Show();
            break;
        case '3':
            ordMenu.Show();
            break;
        case '4':
            invMenu.Show();
            break;
        case '5':
            cityMenu.Show();
            break;
    }
}
while(input!='0');
Console.WriteLine("Au revoir mon ami...");