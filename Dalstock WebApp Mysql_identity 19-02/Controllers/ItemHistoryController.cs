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
    [Authorize]
    public class ItemHistoryController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;
        public ItemHistoryController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            workplaceManager = new WorkplaceManager(uow);
            itemManager = new ItemManager(uow);
        }
        // GET: ItemHistory
        public ActionResult Index()
        {
            ItemHistoryViewModel itemHistory = new ItemHistoryViewModel();
            itemHistory.Debits = itemManager.GetDebits().ToList();
            itemHistory.Deposits = itemManager.GetDeposits().ToList();
            itemHistory.Workplaces = workplaceManager.GetWorkplaces().ToList();
            return View(itemHistory);
        }
    }
}