using BL;
using BL.Managers;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    public class BobbinDebitController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;

        public BobbinDebitController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            workplaceManager = new WorkplaceManager(uow);
            itemManager = new ItemManager(uow);
        }
        // GET: BobbinDebit
        public ActionResult Index()
        {
            return View();
        }

        // GET: BobbinDebit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BobbinDebit/Create
        [HttpGet]
        public ActionResult Create(int bobbinId)
        {
            BobbinDetailViewModel bdvmm = new BobbinDetailViewModel();
            bdvmm.Workplaces = workplaceManager.GetWorkplaces().ToList();
            bdvmm.Bobbin = itemManager.GetBobbin(bobbinId);
            return PartialView("~/Views/Bobbin/_AddBobbinDebit.cshtml", bdvmm);
        }

        // POST: BobbinDebit/Create
        [HttpPost]
        public ActionResult Create(BobbinDetailViewModel bdvmm)
        {
            if (ModelState.IsValid)
            {
                var startIndex = bdvmm.BobbinDebit.StartIndex;
                var endIndex = bdvmm.BobbinDebit.EndIndex;
                int amountUsed;

                if (startIndex > endIndex)
                    amountUsed = startIndex - endIndex;
                else
                    amountUsed = endIndex - startIndex;

                bdvmm.BobbinDebit.AmountUsed = amountUsed;
                itemManager.AddBobbinDebit(bdvmm.BobbinDebit);
                return Json("True");
            }
            else
            {
                bdvmm.Workplaces = workplaceManager.GetWorkplaces().ToList();
                bdvmm.Bobbin = itemManager.GetBobbin(bdvmm.BobbinDebit.BobbinId);
                return PartialView("~/Views/Bobbin/_AddBobbinDebit.cshtml", bdvmm);
            }
        }

        // GET: BobbinDebit/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BobbinDebit/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BobbinDebit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BobbinDebit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
