using AutoMapper;
using Business_Logic.Abstract;
using Course_Project_MVC.Controllers;
using Course_Project_MVC.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ControllerTests
{
    public class NotCoolTests
    {
        private Mock<MyIAuthenticationService> authenticationServiceMock;
        private Mock<IMapper> mapperMock;
        private Mock<IInventoryManager> inventoryManagerMock;
        private AccountController accountController;
        private OrderController orderController;
        private InventoryController inventoryController;
        [SetUp]
        public void Setup()
        {
            mapperMock = new Mock<IMapper>();
            inventoryManagerMock = new Mock<IInventoryManager>();
            authenticationServiceMock = new Mock<MyIAuthenticationService>();
            accountController = new AccountController(authenticationServiceMock.Object);
            orderController = new OrderController(inventoryManagerMock.Object, mapperMock.Object);
            inventoryController = new InventoryController(inventoryManagerMock.Object, mapperMock.Object);
        }

        [Test]
        public async Task AccountControllerTest()
        {
            //string password = "password";
            //string username = "uN";
            //var loginModel = new LoginModel
            //{
            //    Password = password,
            //    UserName = username
            //};
            //var manager = new Manager
            //{
            //    UserName = username,
            //    ManagerID = 1,
            //    FirstName = "fN",
            //    LastName = "lN"
            //};
            //authenticationServiceMock.Setup(m => m.Authentication(username, password)).Returns(manager);
            //var result = await accountController.Login(loginModel,"SomeURL");
            //authenticationServiceMock.Verify(m=>m.Authentication(username,password),Times.Once);
            //Assert.IsInstanceOf<Task<RedirectToActionResult>>(result);

            //this knucklehead fails at httpscontext null reference and i guess i can only make anonymous tests
            Assert.Fail();
        }
        [Test]
        public void OrderControllerTest()
        {
            var orders = new List<Order>
            {
                new Order
                {
                    Count = 1,
                    ManagerID = 1,
                    WareID = 1,
                    Date = DateTime.Now,
                    OrderID = 1,
                },
                new Order
                {
                    Count = 2,
                    ManagerID = 2,
                    WareID = 2,
                    Date = DateTime.Now,
                    OrderID = 2,
                },
                new Order
                {
                    Count = 3,
                    ManagerID = 3,
                    WareID = 3,
                    Date = DateTime.Now,
                    OrderID = 3,
                }
            };
            inventoryManagerMock.Setup(m => m.GetAllOrders()).Returns(orders);

            var result = orderController.Index();


            inventoryManagerMock.Verify(m=>m.GetAllOrders(),Times.Once);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void InventoryControllerTest()
        {

            Assert.Fail();
        }
        [TearDown]
        public void TearDown()
        {
            accountController.Dispose();
            orderController.Dispose();
            inventoryController.Dispose();
        }
    }
}