using Business_Logic.Abstract;
using Business_Logic.Beton;
using DAL.Abstract;
using DAL.Beton;
using DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Data.SqlClient;
using WPF.ViewModels;

namespace BusinessLogicTests
{
    public class SomeTests
    {
        private Mock<IWareDAL> wareDALMock;
        private Mock<IManagerDAL> managerDALMock;
        private Mock<IWareInventoryDAL> wareInventoryDALMock;
        private Mock<IOrderDAL> orderDALMock;
        private IInventoryManager inventoryManager;
        public SomeTests()
        {
            /*
            IConfiguration config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("config.json")
           .Build();

            connectionString = config.GetConnectionString("InventoryManager_TESTS");
            managerDAL = new ManagerDAL(connectionString);
            orderDAL = new OrderDAL(connectionString);
            wareDAL = new WareDAL(connectionString);
            wareInventoryDAL = new WareInventoryDAL(connectionString);

            connection = new SqlConnection(connectionString);
            inventoryManager = new InventoryManager(managerDAL, orderDAL, wareDAL, wareInventoryDAL);
            */
        }
        [SetUp]
        public void SetUp()
        {
            wareDALMock = new Mock<IWareDAL>();
            managerDALMock = new Mock<IManagerDAL>();
            wareInventoryDALMock = new Mock<IWareInventoryDAL>();
            orderDALMock = new Mock<IOrderDAL>();
            inventoryManager = new InventoryManager(managerDALMock.Object,
                                        orderDALMock.Object,
                                        wareDALMock.Object,
                                        wareInventoryDALMock.Object);
        }
        [Test]
        public void CommitOrderTest()
        {
            var tblOrder = new tblOrder
            {
                OrderID = 1,
                ManagerID = 1,
                ManagerUserName = "TestManager",
                WareID = 1,
                WareName = "TestWare",
                Count = 42
            };
            var wareInventory = new WareInventory
            {
                Count = tblOrder.Count,
                WareID = tblOrder.WareID,
                WareName = tblOrder.WareName
            };

            wareInventoryDALMock.Setup(m => m.GetByID(tblOrder.WareID)).Returns((WareInventory)null);
            inventoryManager.CommitOrder(tblOrder);

            wareInventoryDALMock.Verify(m => m.Add(wareInventory), Times.Once);
            wareInventoryDALMock.Verify(m => m.Update(new WareInventory()), Times.Never);
            wareInventoryDALMock.Verify(m => m.DeleteByID(tblOrder.WareID), Times.Never);


            var existingWareInventory = new WareInventory
            {
                Count = 24,
                WareID = tblOrder.WareID,
                WareName = tblOrder.WareName
            };
            var updatedWareInventory = new WareInventory
            {
                Count = Convert.ToInt16(existingWareInventory.Count + wareInventory.Count),
                WareID = tblOrder.WareID,
                WareName = tblOrder.WareName
            };


            wareInventoryDALMock.Setup(m => m.GetByID(tblOrder.WareID)).Returns(existingWareInventory);
            inventoryManager.CommitOrder(tblOrder);



            wareInventoryDALMock.Verify(m => m.Update(updatedWareInventory), Times.Once);
            wareInventoryDALMock.Verify(m => m.Add(new WareInventory()), Times.Never);
            wareInventoryDALMock.Verify(m => m.DeleteByID(tblOrder.WareID), Times.Never);
        }
        [Test]
        public void UpdateOrderTest()
        {
            var orderToUpdate = new Order
            {
                Count = 42,
                WareID = 1,
                ManagerID = 1,
                Date = DateTime.Now,
                OrderID = 1
            };
            //update doesnt return a thing, so the setup foir orderDALMock IS USELESS!
            inventoryManager.UpdateOrder(orderToUpdate);
            orderDALMock.Verify(m => m.Update(orderToUpdate), Times.Once);
            orderDALMock.Verify(m => m.Add(orderToUpdate), Times.Never);
            orderDALMock.Verify(m => m.DeleteByID(orderToUpdate.OrderID), Times.Never);
            orderDALMock.Verify(m => m.GetByID(orderToUpdate.OrderID), Times.Never);
        }
        [Test]
        public void PlaceOrderTest()
        {
            var order = new Order
            {
                WareID = 1,
                Count = 42,
                ManagerID = 1,
                Date = DateTime.Now,
                OrderID = 1
            };
            orderDALMock.Setup(m=>m.Add(order)).Returns(order);
            inventoryManager.PlaceOrder(order);
            orderDALMock.Verify(m=>m.Add(order), Times.Once);
            orderDALMock.Verify(m => m.DeleteByID(order.OrderID), Times.Never);
            orderDALMock.Verify(m=>m.Update(order), Times.Never);
            orderDALMock.Verify(m=>m.GetByID(order.OrderID), Times.Never);
        }
        /*
        [Test]
        public void GetAllWaresTest()
        {
            var wares = new List<Ware>
            {
                AddWareToDBPlusReturn(1),
                AddWareToDBPlusReturn(2),
                AddWareToDBPlusReturn(3),
                AddWareToDBPlusReturn(4)
            };
            var dbWares = inventoryManager.GetAllWares();
            for (int i = 0; i < wares.Count; i++)
                wareDAL.DeleteByID(wares[i].WareID);
            for(int i = 0;i < dbWares.Count; i++)
                Assert.AreEqual(wares[i], dbWares[i]);
        }
        */

        /*
        [Test]
        public void GetAllOrdersTest()
        {
            var orders = new List<Order>
            {
                AddOrderToDBPlusReturn(1),
                AddOrderToDBPlusReturn(2),
                AddOrderToDBPlusReturn(3),
                AddOrderToDBPlusReturn(4)
            };
            var dbOrders = inventoryManager.GetAllOrders();
            for(int i = 0;i<orders.Count;i++)
                orderDAL.DeleteByID(orders[i].OrderID);
            for(int i = 0;i<orders.Count;i++)
                Assert.AreEqual(orders[i], dbOrders[i]);
        }
        */

        /*
        [Test]
        public void PlaceOrderTest()
        {
            var order = new Order
            {
                WareID = 1,
                ManagerID = 1,
                Count = 1,
                Date = DateTime.Now,
            };
            order = inventoryManager.PlaceOrder(order);
            var dbOrder = inventoryManager.GetAllOrders().FirstOrDefault();
            orderDAL.DeleteByID(order.OrderID);
            Assert.AreEqual(dbOrder, order);
        }
        */

        /*
        [Test]
        public void DiscardOrderTest()
        {
            var newOrder = AddOrderToDBPlusReturn(1);
            inventoryManager.DiscardOrderByID(newOrder.OrderID);
            var order = inventoryManager.GetAllOrders().FirstOrDefault();
            Assert.IsNull(order);
        }
        */

        /*
        [Test]
        public void UpdateOrderTest()
        {
            var order = AddOrderToDBPlusReturn(1);
            var newOrder = order;
            newOrder.ManagerID = 2;
            newOrder.Count = 2;
            newOrder.WareID = 2;
            inventoryManager.UpdateOrder(newOrder);
            var updatedOrder = inventoryManager.GetAllOrders().FirstOrDefault();
            orderDAL.DeleteByID(order.OrderID);
            Assert.AreEqual(updatedOrder,newOrder);
        }
        */

        /*
        [Test]
        public void GetAllWareInventoryTest()
        {
            var inventoryItems = new List<WareInventory>
            {
                AddWareInventoryItemToDBPlusReturn(1),
                AddWareInventoryItemToDBPlusReturn(2),
                AddWareInventoryItemToDBPlusReturn(3)
            };
            var dbInventoryItems = inventoryManager.GetAllWareInventory();
            for(int i = 0; i < dbInventoryItems.Count; i++)
                wareInventoryDAL.DeleteByID(inventoryItems[i].WareID);
            for(int i = 0;i < dbInventoryItems.Count; i++)
                Assert.AreEqual(inventoryItems[i], dbInventoryItems[i]);
        }
        */

        /*
        [Test]
        public void CommitOrderTest()
        {
            var order = AddOrderToDBPlusReturn(1);
            var inventoryItem = new WareInventory
            {
                WareID = order.WareID,
                WareName = "1",
                Count = 1
            };
            tblOrder tblOrder = (new tblOrder
            {
                OrderID = order.OrderID,
                WareID = order.WareID,
                WareName = "1",
                ManagerID = order.ManagerID,
                Count = 1,
            });
            inventoryManager.CommitOrder(tblOrder);
            var mustBeNullOrder = inventoryManager.GetAllOrders().FirstOrDefault();
            Assert.IsNull(mustBeNullOrder);
            var dbInventoryItem = inventoryManager.GetAllWareInventory().FirstOrDefault();
            Assert.AreEqual(dbInventoryItem, inventoryItem);
            //----------------------------------------------------------------
            //update existing inventoryItem
            order.OrderID++;
            order.Count = 50;
            tblOrder.OrderID++;
            tblOrder.Count = 50;

            inventoryManager.PlaceOrder(order);
            inventoryManager.CommitOrder(tblOrder);
            inventoryItem.Count = 51;
            dbInventoryItem = inventoryManager.GetAllWareInventory().FirstOrDefault();
            wareInventoryDAL.DeleteByID(order.WareID);
            Assert.AreEqual(inventoryItem,dbInventoryItem);
        }
        */

        /*
        [Test]
        public void GetAllManagersTest()
        {
            //i've done it in the dal section (and it was working) but now its just the same plus different typre of password and added salt
            Assert.Fail();
        }
        */
        /*
        public Manager AddManagerToDBPlusReturn(short number)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                connection.Open();
                cmd.CommandText = $"INSERT INTO tblManager (firstName, lastName, userName, password) OUTPUT INSERTED.managerID VALUES ('fN{number}', 'lN{number}', 'uN{number}', 'pw{number}')";
                int id = (int)cmd.ExecuteScalar();
                connection.Close();
                return new Manager
                {
                    ManagerID = (short)id,
                    FirstName = $"fN{number}",
                    LastName = $"lN{number}",
                    UserName = $"uN{number}",
                    Password = $"pw{number}",
                };
            }
        }
        */
        /*
        public Ware AddWareToDBPlusReturn(short ID)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                Ware newWare = new Ware
                {
                    WareName = $"wareName{ID}",
                    WareDescription = $"wareDescription{ID}",
                    Cost = ID
                };
                cmd.CommandText = $"INSERT INTO tblWare (wareName,wareDescription,wareCost) output inserted.wareID VALUES ('wareName{ID}','wareDescription{ID}',{ID})";
                connection.Open();
                newWare.WareID = Convert.ToInt16(cmd.ExecuteScalar());
                connection.Close();
                return newWare;
            }
        }
        */
        /*
        public Order AddOrderToDBPlusReturn(short ID)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                Order newOrder= new Order
                {
                    Count = ID,
                    ManagerID = ID,
                    WareID =ID,
                    Date = DateTime.Now,
                };
                cmd.CommandText = $"INSERT INTO tblOrder (wareID,managerID,count,date) output inserted.orderID VALUES ({ID},{ID},{ID},@date)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("date",newOrder.Date);
                connection.Open();
                newOrder.OrderID = Convert.ToInt16(cmd.ExecuteScalar());
                connection.Close();
                return newOrder;
            }
        }
        */
        /*
        public WareInventory AddWareInventoryItemToDBPlusReturn(short ID)
        {
            using (SqlCommand cmd = connection.CreateCommand())
            {
                WareInventory newItem = new WareInventory
                {
                    Count = ID,
                    WareID = ID,
                    WareName = ID.ToString()
                };
                cmd.CommandText = $"INSERT INTO tblInventory (wareID,wareName,count) VALUES ({ID},'{ID}',{ID})";
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                return newItem;
            }
        }
        */
    }
}