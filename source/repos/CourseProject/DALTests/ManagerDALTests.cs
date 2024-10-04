using DAL.Beton;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DALTests
{
    public class ManagerDALTests
    {
        private readonly ManagerDAL DAL;
        private readonly List<Manager> managers;
        private readonly string connectionString;
        private readonly SqlConnection connection;
        public ManagerDALTests()
        {
            IConfiguration config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("config.json")
           .Build();

            connectionString = config.GetConnectionString("InventoryManager_TESTS");
            DAL = new ManagerDAL(connectionString);
            connection = new SqlConnection(connectionString);
            managers = new List<Manager>();
        }
        [SetUp] //before everytest
        public void Setup()
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                managers.Add(AddManagerToDBPlusReturn(1));
                managers.Add(AddManagerToDBPlusReturn(2));
                managers.Add(AddManagerToDBPlusReturn(3));
            }
        }

        [TearDown] //after everytest
        public void TearDown()
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = "DELETE FROM tblManager WHERE managerID = 1";
                cmd.ExecuteNonQuery();
                connection.Close();

                connection.Open();
                cmd.CommandText = "DELETE FROM tblManager WHERE managerID = 2";
                cmd.ExecuteNonQuery();
                connection.Close();

                connection.Open();
                cmd.CommandText = "DELETE FROM tblManager WHERE managerID = 3";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        [Test]
        public void DeleteTest()
        {
            DAL.DeleteByID(managers[0].ManagerID);
            DAL.DeleteByID(managers[1].ManagerID);
            DAL.DeleteByID(managers[2].ManagerID); //start by getByID then add then delete then update then go home and relax
                                                   //(if you're already home then don't even try to do this)

            //Assert.IsTrue(managers.Count)
        }
        [Test]
        public void GetByIDTest()
        {
            Manager actual = managers[0];
            Manager expected = DAL.GetByID(managers[0].ManagerID);
            Assert.AreEqual(actual,expected);
        }
        public Manager AddManagerToDBPlusReturn(short number)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = $"INSERT INTO tblManager (firstName, lastName, userName, password, inventoryID) OUTPUT INSERTED.managerID VALUES ('fN{number}', 'lN{number}', 'uN{number}', 'pw{number}', {number})";
                int id =  (int)cmd.ExecuteScalar();
                connection.Close();
                return new Manager
                {
                    ManagerID = (short)id,
                    FirstName = $"fn{number}",
                    LastName = $"lN{number}",
                    UserName = $"uN{number}",
                    Password = $"pw{number}",
                    InventoryID = number
                };
            }
        }
        public int DeleteManager(short id)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"DELETE FROM tblManager WHERE managerID = {id}";
                return (int)cmd.ExecuteNonQuery();
            }
        }
    }
}