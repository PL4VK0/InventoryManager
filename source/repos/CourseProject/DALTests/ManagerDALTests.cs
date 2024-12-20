//using Business_Logic.Beton;
//using DAL.Beton;
//using DTO;
//using Microsoft.Extensions.Configuration;
//using System.Data.SqlClient;

//namespace DALTests
//{
//    public class ManagerDALTests
//    {
//        private readonly ManagerDAL managerDAL;
//        private readonly OrderDAL orderDAL;
//        private readonly WareDAL wareDAL;
//        private readonly WareInventoryDAL wareInventoryDAL;
//        private readonly List<Manager> managers;
//        private readonly string connectionString;
//        private SqlConnection connection;
//        private readonly InventoryManager inventoryManager;
//        public ManagerDALTests()
//        {
//            IConfiguration config = new ConfigurationBuilder()
//           .SetBasePath(Directory.GetCurrentDirectory())
//           .AddJsonFile("config.json")
//           .Build();

//            connectionString = config.GetConnectionString("InventoryManager_TESTS");
//            managerDAL = new ManagerDAL(connectionString);
//            orderDAL = new OrderDAL(connectionString);
//            wareDAL = new WareDAL(connectionString);
//            wareInventoryDAL = new WareInventoryDAL(connectionString);

//            connection = new SqlConnection(connectionString);
//            inventoryManager = new InventoryManager(managerDAL, orderDAL, wareDAL, wareInventoryDAL);
//            managers = new List<Manager>();
//        }
//        [SetUp] //before everytest
//        public void Setup()
//        {
//            managers.Clear();
//            managers.Add(AddManagerToDBPlusReturn(1));
//            managers.Add(AddManagerToDBPlusReturn(2));
//            managers.Add(AddManagerToDBPlusReturn(3));
//        }

//        [TearDown] //after everytest
//        public void TearDown()
//        {
//            using (SqlCommand cmd = connection.CreateCommand())
//            {
//                cmd.CommandText = $"DELETE FROM tblManager WHERE 1 = 1";
//                connection.Open();
//                cmd.ExecuteNonQuery();
//                connection.Close();
//            }
//        }

//        [Test]
//        public void DeleteTest()
//        {
//            managerDAL.DeleteByID(managers[0].ManagerID);
//            managerDAL.DeleteByID(managers[1].ManagerID);
//            managerDAL.DeleteByID(managers[2].ManagerID); //start by getByID then add then delete then update then go home and relax
//                                                   //(if you're already home then don't even try to do this)
//            short count = 0;
//            for (int i = 0; i < 3; i++)
//            {
//                Manager manager = GetByID(managers[i].ManagerID);
//                if (manager != null)
//                    count++;
//            }
//            Assert.IsTrue(count == 0);
//        }
//        [Test]
//        public void GetByIDTest()
//        {
//            Manager expected = managers[0];
//            Manager actual = managerDAL.GetByID(managers[0].ManagerID);
//            Assert.AreEqual(expected, actual);
//        }


//        [Test]
//        public void AddTest()
//        {
//            var addedManager = AddManagerToDBPlusReturn(4);

//            var gotManager = managerDAL.GetByID(addedManager.ManagerID);

//            DeleteManager(addedManager.ManagerID);

//            Assert.AreEqual(addedManager, gotManager);
         
//        }
//        [Test]
//        public void UpdateTest()
//        {
//            string newFN = "newFN";
//            string newLN = "newLN";
//            string newUN = "newUN";
//            string newPW = "newPW";

//            using (SqlCommand cmd = connection.CreateCommand())
//            {
//                cmd.CommandText = $"UPDATE tblManager SET firstName = 'newFN', lastName = 'newLN', userName = 'newUN', password = 'newPW', WHERE managerID = {managers[1].ManagerID}";
//                connection.Open();
//                cmd.ExecuteNonQuery();
//                connection.Close();
//            }
//            Manager updatedManger = new Manager
//            {
//                ManagerID = managers[1].ManagerID,
//                FirstName = newFN,
//                LastName = newLN,
//                UserName = newUN,
//                Password = newPW,
//            };

//            Assert.AreEqual(managerDAL.GetByID(managers[1].ManagerID), updatedManger);

//        }
//        [Test]
//        public void GetAllTest()
//        {
//            List<Manager> gotList = managerDAL.GetAll();

//            for (int i = 0; i < gotList.Count; i++)
//                Assert.AreEqual(managers[i], gotList[i]);
//        }
//        public Manager AddManagerToDBPlusReturn(short number)
//        {
//            connection.Open();
//            using (SqlCommand cmd = connection.CreateCommand())
//            {
//                cmd.CommandText = $"INSERT INTO tblManager (firstName, lastName, userName, password) OUTPUT INSERTED.managerID VALUES ('fN{number}', 'lN{number}', 'uN{number}', 'pw{number}')";
//                int id = (int)cmd.ExecuteScalar();
//                connection.Close();
//                return new Manager
//                {
//                    ManagerID = (short)id,
//                    FirstName = $"fN{number}",
//                    LastName = $"lN{number}",
//                    UserName = $"uN{number}",
//                    Password = $"pw{number}",
//                };
//            }
//        }
//        public int DeleteManager(short id)
//        {
//            using (SqlCommand cmd = connection.CreateCommand())
//            {
//                cmd.CommandText = $"DELETE FROM tblManager WHERE managerID = {id}";
//                connection.Open();
//                int result = cmd.ExecuteNonQuery();
//                connection.Close();
//                return result;
//            }
//        }



//        public Manager GetByID(short id)
//        {
//            using (SqlCommand cmd = connection.CreateCommand())
//            {
//                cmd.CommandText = $"SELECT * FROM tblManager WHERE managerID = {id}";
//                connection.Open();
//                SqlDataReader reader = cmd.ExecuteReader();


//                if (reader.Read())
//                {
//                    var manager = new Manager
//                    {
//                        ManagerID = (short)id,
//                        FirstName = reader.GetString(0),
//                        LastName = reader.GetString(1),
//                        UserName = reader.GetString(2),
//                        Password = reader.GetString(3)
//                    };
//                    connection.Close();
//                    return manager;
//                }
//                connection.Close();
//                return null;
//            }
//        }
//    }
//}