using Business_Logic.Abstract;
using Business_Logic.Beton;
using DAL.Abstract;
using DTO;
using Moq;
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
            inventoryManager.CommitOrder(tblOrder.OrderID);

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
            inventoryManager.CommitOrder(tblOrder.OrderID);
            //commit order returns void so no return chekc


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
            Assert.IsTrue(inventoryManager.UpdateOrder(orderToUpdate));

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

        [Test]
        public void GetAllWaresTest()
        {
            var wares = new List<Ware>
            {
                new Ware
                {
                    WareID = 1,
                    WareDescription = "FooDescription1",
                    WareName = "FooName1"
                },
                new Ware
                {
                    WareID = 2,
                    WareDescription = "FooDescription2",
                    WareName = "FooName2"
                },
                new Ware
                {
                    WareID = 3,
                    WareDescription = "FooDescription3",
                    WareName = "FooName3"
                }
            };
            wareDALMock.Setup(m => m.GetAll()).Returns(wares);

            Assert.That(inventoryManager.GetAllWares(),Is.EqualTo(wares));

            wareDALMock.Verify(m=>m.GetAll(), Times.Once);
        }
        [Test]
        public void GetAllOrdersTest()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    WareID = 1,
                    Count = 1,
                    ManagerID = 1,
                    Date = DateTime.Now,
                    OrderID = 1
                },
                new Order
                {
                    WareID = 2,
                    Count = 2,
                    ManagerID = 2,
                    Date = DateTime.Now,
                    OrderID = 2
                },
                new Order
                {
                    WareID = 3,
                    Count = 3,
                    ManagerID = 3,
                    Date = DateTime.Now,
                    OrderID = 3
                },
            };
            orderDALMock.Setup(m => m.GetAll()).Returns(orders);

            Assert.That(inventoryManager.GetAllOrders(), Is.EqualTo(orders));

            orderDALMock.Verify(m => m.GetAll(), Times.Once);
        }
        [Test]
        public void GetAllWareInventoryTest()
        {
            var invItems = new List<WareInventory>
            {
                new WareInventory
                {
                    WareID = 1,
                    Count = 1,
                    WareName = "Name1"
                },
                new WareInventory
                {
                    WareID = 2,
                    Count = 2,
                    WareName = "Name2"
                },
                new WareInventory
                {
                    WareID = 3,
                    Count = 3,
                    WareName = "Name3"
                },
            };
            wareInventoryDALMock.Setup(m => m.GetAll()).Returns(invItems);

            Assert.That(inventoryManager.GetAllWareInventory(), Is.EqualTo(invItems));

            wareInventoryDALMock.Verify(m => m.GetAll(), Times.Once);
        }
    }
}