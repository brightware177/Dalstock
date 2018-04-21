using BL;
using BL.Managers;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    [Authorize(Roles = "Admin,User,SuperUser")]
    public class DepositController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;
        public DepositController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            uow = new UnitOfWork(database, database);
            workplaceManager = new WorkplaceManager(uow);
            itemManager = new ItemManager(uow);
        }
        // GET: Deposit
        public ActionResult Index()
        {
            var items = itemManager.GetItems().ToList();
            DepositViewModel model = new DepositViewModel();
            model.Items = items;
            return View(model);
        }
        //Makes the templist persistent
        [HttpPost]
        public ActionResult CreateDepositItems(List<DepositTableViewModel> tableViewModelList)
        {
            var staff = workplaceManager.GetStaffWithApplicationUserId(User.Identity.GetUserId());
            List<Deposit> depositList = new List<Deposit>();
            foreach (var model in tableViewModelList)
            {
                var item = itemManager.GetItem(model.ItemId);
                Deposit debit = new Deposit()
                {
                    ItemId = item.Id,
                    Amount = model.Amount,
                    Date = model.Date,
                    Deposited_By_Staff =staff
                };
                depositList.Add(debit);
            }
            itemManager.BulkAddDepositItems(depositList);
            return View();
        }
    }
}