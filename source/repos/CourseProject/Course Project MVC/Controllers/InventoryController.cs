using AutoMapper;
using Business_Logic.Abstract;
using Course_Project_MVC.Models;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course_Project_MVC.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        // GET: InventoryController
        IInventoryManager inventoryManager;
        IMapper mapper;
        public InventoryController(IInventoryManager inventoryManager, IMapper mapper)
        {
            this.inventoryManager = inventoryManager;
            this.mapper = mapper;
        }
        public ActionResult Index()
        {
            var smth = mapper.Map<List<InventoryDetailsModel>>(inventoryManager.GetAllWareInventory());
            return View(smth);
        }

        // GET: InventoryController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: InventoryController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: InventoryController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: InventoryController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: InventoryController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: InventoryController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: InventoryController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
