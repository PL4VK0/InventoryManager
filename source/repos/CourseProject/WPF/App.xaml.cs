using Business_Logic.Abstract;
using Business_Logic.Beton;
using DAL.Beton;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private IServiceProvider serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("config.json").Build();
            string connectionString = config.GetConnectionString("InventoryManager");

            ManagerDAL managerDAL = new ManagerDAL(connectionString);
            WareDAL wareDAL = new WareDAL(connectionString);
            OrderDAL orderDAL = new OrderDAL(connectionString);
            WareInventoryDAL wareInventoryDAL = new WareInventoryDAL(connectionString);
            InventoryManager inventoryManager = new InventoryManager(managerDAL,orderDAL,wareDAL,wareInventoryDAL);
            MyIAuthenticationService authenticationService = new AuthenticationService(managerDAL);
            var startFenetre = new MainWindow(inventoryManager, authenticationService);
            startFenetre.Show();

        }
    }

}
