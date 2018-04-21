using BL;
using DAL.UnitOfWork;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    public class ItemController : Controller
    {
        ItemManagerService itemManager;
        IUnitOfWork uow;

        public ItemController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            uow = new UnitOfWork(database, database);
            itemManager = new ItemManager(uow);
        }
        // GET: Item
        public ActionResult Index()
        {
            var items = itemManager.GetItems().ToList();
            return View(items);
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: Workplace/Create
        [HttpPost]
        public ActionResult Create(Item model)
        {
            if (ModelState.IsValid)
            {
                itemManager.AddItem(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            var itemToEdit = itemManager.GetItem(id);
            return View(itemToEdit);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(Item model)
        {
            if (ModelState.IsValid)
            {
                itemManager.ChangeItem(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Delete(string id)
        {
            var item = itemManager.GetItem(id);
            return RedirectToAction("Index");
        }
    }
}