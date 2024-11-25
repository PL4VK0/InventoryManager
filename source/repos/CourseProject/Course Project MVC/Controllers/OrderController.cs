using AutoMapper;
using Business_Logic.Abstract;
using Course_Project_MVC.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Claims;

namespace Course_Project_MVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        protected readonly IMapper mapper;
        protected readonly IInventoryManager inventoryManager;
        public OrderController(IInventoryManager inventoryManager,IMapper mapper)
        {
            this.mapper = mapper;
            this.inventoryManager = inventoryManager;
        }

        // GET: OrderController
        [AllowAnonymous]
        public ActionResult Index()
        {
            var orders = mapper.Map<List<OrderDetailsModel>>(inventoryManager.GetAllOrders());
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            var smth = new OrderDetailsModel();
            GetRequiredItems(id, smth);
            return View(smth);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            var something = new EditOrderModel();
            GetRequiredItems(something);
            return View(something);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EditOrderModel editModel)
        {
            if(!ModelState.IsValid)
            {
                GetRequiredItems(editModel);
                return View(editModel);
            }
            try
            {
                var managerID = Convert.ToInt16(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var order = mapper.Map<Order>(editModel);
                order.ManagerID = managerID;
                order.Date = DateTime.Now;
                inventoryManager.PlaceOrder(order);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                GetRequiredItems(editModel);
                return View(editModel);
            }
        }


        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            var something = new EditOrderModel();
            GetRequiredItems(id,something);
            return View(something);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditOrderModel model)
        {
            if (!ModelState.IsValid)
            {
                GetRequiredItems(id, model);
                return View(model);
            }
            try
            {
                var managerID = Convert.ToInt16(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var updatedOrder = mapper.Map<Order>(model);
                updatedOrder.ManagerID = managerID;
                updatedOrder.Date = DateTime.Now;
                inventoryManager.UpdateOrder(updatedOrder);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                GetRequiredItems(id, model);
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            var smth = new OrderDetailsModel();
            GetRequiredItems(id, smth);
            return View(smth);
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, OrderDetailsModel model)
        {
            try
            {
                inventoryManager.DiscardOrderByID((short)id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Couldn't delete this order\nTry again later\n(or never)");
                GetRequiredItems((short)id, model);
                return View(model);
            }
        }
        public ActionResult Commit(int id)
        {
            var smth = new OrderDetailsModel();
            GetRequiredItems(id, smth);
            return View(smth);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Commit(int id, OrderDetailsModel model)
        {
            try
            {
                inventoryManager.CommitOrder((short)id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Couldn't commit the order\nTry again later..... or never)");
                GetRequiredItems((short)id, model);
                return View(model);
            }
        }
        private void GetRequiredItems(EditOrderModel editModel)
        {
            editModel.Wares = mapper.Map<List<SelectListItem>>(inventoryManager.GetAllWares());
        }
        private void GetRequiredItems(int id, EditOrderModel model)
        {
            Order order = inventoryManager.GetAllOrders().Find(o => o.OrderID == id)!;
            GetRequiredItems(model);
            model.OrderID = order.OrderID;
            model.WareID = order.WareID;
            model.ManagerID = order.ManagerID;
            model.Count = order.Count;
            model.Date = order.Date;
            model.UserName = order.UserName;
            model.WareName = order.WareName;
        }
        private void GetRequiredItems(int id, OrderDetailsModel model)
        {
            Order order = inventoryManager.GetAllOrders().Find(o => o.OrderID == id)!;
            model.OrderID = order.OrderID;
            model.WareID = order.WareID;
            model.ManagerID = order.ManagerID;
            model.Count = order.Count;
            model.Date = order.Date;
            model.UserName = order.UserName;
            model.WareName = order.WareName;
        }
    }
}
