using DAL.Beton;
using DTO;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .Build();


string connectionString = config.GetConnectionString("InventoryManager");
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
            WareMenu();
            break;
        case '2':
            ManagerMenu();
            break;
        case '3':
            OrderMenu();
            break;
        case '4':
            InventoryMenu();
            break;
        case '5':
            CityMenu();
            break;
    }
}
while(input!='0');
Console.WriteLine("Au revoir mon ami...");

void CityMenu()
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

void UpdateCityByID()
{
    var cityDAL = new CityDAL(connectionString);

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
    cityDAL.Update(updatedCity);
    Console.WriteLine($"Updated to\n{updatedCity}");
}

void DeleteCityByID()
{
    var cityDAL = new CityDAL(connectionString);
    short id;
    ListAllCities();
    Console.WriteLine("Enter id of the city you want to destroy: ");
    id = Convert.ToInt16(Console.ReadLine());
    short deleted = cityDAL.DeleteByID(id);
    if (deleted == 0)
    {
        Console.WriteLine("There were no cities with such id...");
        return;
    }
    Console.WriteLine($"City with {id} id was DESTROYED!");
}

void ListAllCities()
{
    var cityDAL = new CityDAL(connectionString);
    var cities = cityDAL.GetAll();
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

void AddCity()
{
    var cityDAL = new CityDAL(connectionString);

    string cityName;
    Console.WriteLine("Enter cityName: ");
    cityName = Console.ReadLine();

    City city = new City
    {
        CityName = cityName
    };
    City newCity = cityDAL.Add(city);
    Console.WriteLine($"Added\n{newCity}");
}

void InventoryMenu()
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
    var inventoryDAL = new InventoryDAL(connectionString);

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
    inventoryDAL.Update(updatedInventory);
    Console.WriteLine($"Updated to\n{updatedInventory}");
}

void DeleteInventoryByID()
{
    var inventoryDAL = new InventoryDAL(connectionString);

    ListAllInventories();

    short id;
    Console.WriteLine("Enter the id of inventory you want to destroy: ");
    id = Convert.ToInt16(Console.ReadLine());

    short deleted = inventoryDAL.DeleteByID(id);
    if (deleted == 0)
    {
        Console.WriteLine("No inventory was found with such id...");
        return;
    }
    Console.WriteLine($"Deleted inventory with id {id}...");
}

void ListAllInventories()
{
    var inventoryDAL = new InventoryDAL(connectionString);

    var inventories = inventoryDAL.GetAll();

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
    var inventoryDAL = new InventoryDAL(connectionString);
    short cityID;

    Console.WriteLine("Enter id of the city: ");
    cityID = Convert.ToInt16(Console.ReadLine());

    var inventory = new Inventory
    { CityID = cityID };
    var newInventory = inventoryDAL.Add(inventory);
    Console.WriteLine($"Added\n{newInventory}");
}

void OrderMenu()
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
    var orderDAL = new OrderDAL(connectionString);

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
        InventoryID = inventoryID
    };
    orderDAL.Update(updatedOrder);
    Console.WriteLine($"Updated to\n{updatedOrder}");
}

void DeleteOrderByID()
{
    var orderDAL = new OrderDAL(connectionString);

    ListAllOrders();

    short id;
    Console.WriteLine("Enter orderID you want gone: ");
    id = Convert.ToInt16(Console.ReadLine());

    short deleted = orderDAL.DeleteByID(id);
    if (deleted == 0)
    {
        Console.WriteLine("No order was found with such id...");
        return;
    }
    Console.WriteLine($"Deleted order with id {id}. Goodbye...");
}

void ListAllOrders()
{
    var orderDAL = new OrderDAL(connectionString);

    var orders = orderDAL.GetAll();

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
    var orderDAL = new OrderDAL(connectionString);
    short managerID;
    short inventoryID;

    Console.WriteLine("Enter managerID that is making the order: ");
    managerID = Convert.ToInt16(Console.ReadLine());
    Console.WriteLine("Enter invenotryID where the order is going: ");
    inventoryID = Convert.ToInt16(Console.ReadLine());

    var order = new Order
    {
        ManagerID = managerID,
        InventoryID = inventoryID
    };
    var newOrder = orderDAL.Add(order);
    Console.WriteLine($"Added\n{newOrder}");
}

void ManagerMenu()
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
    var managerDAL = new ManagerDAL(connectionString);

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
    inventoryID = Convert.ToInt16(Console.ReadLine());

    Manager manager = new Manager
    {
        ManagerID = id,
        FirstName = firstName,
        LastName = lastName,
        UserName = userName,
        Password = password,
        InventoryID = inventoryID
    };
    managerDAL.Update(manager);

    Console.WriteLine($"Updated to\n{manager}");
}

void DeleteManagerByID()
{
    var managerDAL = new ManagerDAL(connectionString);

    ListAllManagers();

    short id;
    Console.WriteLine("Enter the id of manager you want gone: ");
    id = Convert.ToInt16(Console.ReadLine());

    short deleted = managerDAL.DeleteByID(id);
    if(deleted == 0)
    {
        Console.WriteLine("No manager was found with such id...");
        return;
    }
    Console.WriteLine($"Deleted manager with id {id}. Goodbye...");
}

void ListAllManagers()
{
    var managerDAL = new ManagerDAL(connectionString);

    var managers = managerDAL.GetAll();

    if(managers.Count==0)
    {
        Console.WriteLine("There are no managers... (nobody wants to work for us)");
        return;
    }
    foreach( var manager in managers)
    {
        Console.WriteLine(manager);
    }
}

void AddManager()
{
    var managerDAL = new ManagerDAL(connectionString);

    string firstName;
    string lastName;
    string userName;
    string password;
    short inventoryID;


    Console.WriteLine("Enter firstName: ");
    firstName = Console.ReadLine();
    Console.WriteLine("Enter lastName: ");
    lastName = Console.ReadLine();
    Console.WriteLine("Enter userName: ");
    userName = Console.ReadLine();
    Console.WriteLine("Enter password: ");
    password = Console.ReadLine();
    Console.WriteLine("Enter inventoryID: ");
    inventoryID = Convert.ToInt16(Console.ReadLine());

    Manager manager = new Manager
    {
        FirstName = firstName,
        LastName = lastName,
        UserName  = userName,
        Password = password,
        InventoryID = inventoryID
    };
    Manager newManager = managerDAL.Add(manager);

    Console.WriteLine($"Added\n{newManager}");
}

void WareMenu()
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

        switch (input )
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
    var wareDAL = new WareDAL(connectionString);

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
    wareDAL.Update(updatedWare);
    Console.WriteLine($"Updated to\n{updatedWare}");
}

void DeleteWareByID()
{
    var wareDAL = new WareDAL(connectionString);
    short id;
    ListAllWares();
    Console.WriteLine("Enter id of the ware you want gone: ");
    id = Convert.ToInt16(Console.ReadLine());
    short deleted = wareDAL.DeleteByID(id);
    if(deleted==0)
    {
        Console.WriteLine("There were no wares with such id...");
        return;
    }
    Console.WriteLine($"ware with {id} id was deleted!");
}

void ListAllWares()
{
    var wareDAL = new WareDAL(connectionString);
    var wares = wareDAL.GetAll();
    if(wares.Count==0)
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
    var wareDAL = new WareDAL(connectionString);

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
    Ware newWare = wareDAL.Add(ware);

    Console.WriteLine($"Added\n{newWare}");
}