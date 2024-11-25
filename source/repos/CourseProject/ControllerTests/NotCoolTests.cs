using AutoMapper;
using Business_Logic.Abstract;
using Course_Project_MVC.Controllers;
using Course_Project_MVC.Models;
using DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using System.Security.Claims;

namespace ControllerTests
{
    public class NotCoolTests
    {
        Mock<MyIAuthenticationService> myAuthenticationServiceMock;
        Mock<IUrlHelper> urlHelperMock;
        Mock<IAuthenticationService> authenticationServiceMock;
        Mock<IMapper> mapperMock;
        Mock<IInventoryManager> inventoryManagerMock;
        AccountController accountController;
        OrderController orderController;
        InventoryController inventoryController;
        Mock<HttpContext> httpContextMock;
        List<Claim> claims;
        ClaimsPrincipal principal;
        [SetUp]
        public void Setup()
        {
            httpContextMock = new Mock<HttpContext>();
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,"1"),
                new Claim(ClaimTypes.Name,"DILAN"),
                new Claim(ClaimTypes.Role,"RULER")
            };
            principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            httpContextMock.Setup(m=>m.User).Returns(principal);
            mapperMock = new Mock<IMapper>();
            urlHelperMock = new Mock<IUrlHelper>();
            inventoryManagerMock = new Mock<IInventoryManager>();
            myAuthenticationServiceMock = new Mock<MyIAuthenticationService>();
            authenticationServiceMock = new Mock<IAuthenticationService>();
            accountController = new AccountController(myAuthenticationServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextMock.Object
                },
                Url = urlHelperMock.Object,
            };
            orderController = new OrderController(inventoryManagerMock.Object, mapperMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContextMock.Object
                }
            };
            inventoryController = new InventoryController(inventoryManagerMock.Object, mapperMock.Object);
        }
        [Test]
        public async Task AccountControllerFailedLoginTest()
        {
            string password = "password";
            string username = "uN";
            var loginModel = new LoginModel
            {
                Password = password,
                UserName = username
            };

            myAuthenticationServiceMock.Setup(m => m.Authentication(username, password)).Returns((Manager?)null);
            var newResult = await accountController.Login(loginModel, "SomeURL");
            Assert.IsInstanceOf<ViewResult>(newResult);
            Assert.IsTrue((newResult as ViewResult)!.Model == loginModel);
            Assert.IsTrue(accountController.ModelState.ErrorCount==1);

        }
        [Test]
        public async Task AccountControllerLoginTest()
        {
            string password = "password";
            string username = "uN";
            var loginModel = new LoginModel
            {
                Password = password,
                UserName = username
            };
            var manager = new Manager
            {
                UserName = username,
                ManagerID = 1,
                FirstName = "fN",
                LastName = "lN"
            };
            var result = accountController.Login("SomeURL");
            Assert.IsInstanceOf<ViewResult>(result);

            myAuthenticationServiceMock.Setup(m => m.Authentication(username, password)).Returns(manager);
            httpContextMock
                .Setup(h => h.RequestServices.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);


            urlHelperMock.Setup(m=>m.IsLocalUrl("SomeURL")).Returns(true);
            var newResult =await accountController.Login(loginModel, "SomeURL");

            myAuthenticationServiceMock.Verify(m => m.Authentication(username, password), Times.Once);
            Assert.IsInstanceOf<RedirectResult>(newResult);
            Assert.IsTrue((newResult as RedirectResult)!.Url == "SomeURL");
        }
        [Test]
        public async Task AccountControllerLogoutTest()
        {
            httpContextMock
                .Setup(h => h.RequestServices.GetService(typeof(IAuthenticationService)))
                .Returns(authenticationServiceMock.Object);
            var result =await accountController.Logout();
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.IsTrue((result as RedirectToActionResult)!.ActionName == "Index");
            Assert.IsTrue((result as RedirectToActionResult)!.ControllerName == "Home");
        }
        [Test]
        public void OrderControllerIndexTest()
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


            inventoryManagerMock.Verify(m => m.GetAllOrders(), Times.Once);
            Assert.IsInstanceOf<ViewResult>(result);
        }
        [Test]
        public void OrderControllerCommitTest()
        {
            var order = new Order
            {
                OrderID = 3,
                Count = 1,
                Date = DateTime.Now,
                WareID = 1,
                ManagerID = 1
            };
            List<Order> orders = new List<Order>
            {
                order
            };
            inventoryManagerMock.Setup(m => m.GetAllOrders()).Returns(orders);
            var result = orderController.Commit(order.OrderID);
            inventoryManagerMock.Verify(m => m.GetAllOrders(),Times.Once);
            Assert.IsInstanceOf<ViewResult>(result);

            var orderDetails = new OrderDetailsModel
            {
                OrderID = order.OrderID,
                ManagerID = order.ManagerID,
                WareID = order.WareID,
                Count = order.Count,
                Date = order.Date
            };
            var newResult = orderController.Commit(order.OrderID, orderDetails);
            inventoryManagerMock.Verify(m=>m.CommitOrder(order.OrderID), Times.Once);

            Assert.IsInstanceOf<RedirectToActionResult>(newResult);
            Assert.IsTrue((newResult as RedirectToActionResult)!.ActionName == "Index");
        }
        [Test]
        public void OrderControllerEditTest()
        {
            var order = new Order
            {
                OrderID = 3,
                Count = 1,
                Date = DateTime.Now,
                WareID = 1,
                ManagerID = 1
            };
            List<Order> orders = new List<Order>
            {
                order
            };
            List<Ware> wares = new List<Ware>
            {
                new Ware
                {
                    WareID=1,
                    WareName = "testName",
                    WareDescription = "testDescription"
                }
            };
            inventoryManagerMock.Setup(m=>m.GetAllOrders()).Returns(orders);
            inventoryManagerMock.Setup(m=>m.GetAllWares()).Returns(wares);

            var result = orderController.Edit(order.OrderID);
            inventoryManagerMock.Verify(m=>m.GetAllOrders(), Times.Once);
            inventoryManagerMock.Verify(m=>m.GetAllWares(), Times.Once);
            Assert.IsInstanceOf<ViewResult>(result);

            var editModel = new EditOrderModel
            {
                OrderID = order.OrderID,
                ManagerID = order.ManagerID,
                Count = order.Count,
                WareID = order.WareID,
                Wares = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "testText",
                        Value = wares.FirstOrDefault()!.WareID.ToString(),
                    }
                }
            };
            editModel.Count = 42;
            var updatedOrder = new Order
            {
                OrderID = order.OrderID,
                WareID = order.WareID,
                ManagerID = order.ManagerID,
                Count = 42,
                Date = order.Date
            };
            mapperMock.Setup(m => m.Map<Order>(editModel)).Returns(updatedOrder);
            var newResult = orderController.Edit(order.OrderID, editModel);

            inventoryManagerMock.Verify(m => m.UpdateOrder(updatedOrder), Times.Once);
            Assert.IsInstanceOf<RedirectToActionResult> (newResult);
        }
        [Test]
        public void OrderControllerDeleteTest()
        {
            var order = new Order
            {
                OrderID = 3,
                Count = 1,
                Date = DateTime.Now,
                WareID = 1,
                ManagerID = 1
            };
            List<Order> orders = new List<Order>
            {
                order
            };
            List<Ware> wares = new List<Ware>
            {
                new Ware
                {
                    WareID=1,
                    WareName = "testName",
                    WareDescription = "testDescription"
                }
            };
            inventoryManagerMock.Setup(m => m.GetAllOrders()).Returns(orders);
            inventoryManagerMock.Setup(m => m.GetAllWares()).Returns(wares);

            var result = orderController.Delete(order.OrderID);
            Assert.IsInstanceOf<ViewResult>(result);

            var detailsModel = new OrderDetailsModel
            {
                OrderID = order.OrderID,
                ManagerID = order.ManagerID,
                Count = order.Count,
                WareID = order.WareID
            };
            var newResult = orderController.Delete(order.OrderID, detailsModel);
            inventoryManagerMock.Verify(m=>m.DiscardOrderByID(order.OrderID),Times.Once);
            Assert.IsInstanceOf<RedirectToActionResult>(newResult);
            Assert.IsTrue((newResult as RedirectToActionResult)!.ActionName == "Index");
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