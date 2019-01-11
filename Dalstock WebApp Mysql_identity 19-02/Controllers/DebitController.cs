using BL;
using DAL;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DAL.UnitOfWork;
using BL.Managers;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    [Authorize(Roles = "Admin,User,SuperUser")]
    public class DebitController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;
        DebitViewModel model = new DebitViewModel();
        public DebitController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            workplaceManager = new WorkplaceManager(uow);
            itemManager = new ItemManager(uow);
        }
        // GET: Debit
        public ActionResult Index()
        {
            var workplaces = workplaceManager.GetWorkplaces().ToList();
            var items = itemManager.GetItems().ToList();
            model.Workplaces = workplaces;
            model.Items = items;
            return View(model);
        }
        // GET: Debit
        public ActionResult IndexWithWorkplaceId(string id)
        {
            var workplaces = workplaceManager.GetWorkplaces().ToList();
            var items = itemManager.GetItems().ToList();
            model.Workplaces = workplaces;
            model.Items = items;
            model.SelectedWorkplaceId = id;
            return View("Index",model);
        }
        [HttpGet]
        public PartialViewResult GetWorkplace(string id)
        {
            var workplace = workplaceManager.GetWorkplace(id);
            return PartialView("_WorkplaceDetail", workplace);
        }

        //Makes the templist persistent
        [HttpPost]
        public ActionResult CreateDebitItems(List<TableViewModel> tableViewModelList)
        {
            var staff = workplaceManager.GetStaffWithApplicationUserId(User.Identity.GetUserId());
            List<Debit> debitList = new List<Debit>();
            foreach (var model in tableViewModelList)
            {
                var item = itemManager.GetItem(model.ItemId);
                var workplace = workplaceManager.GetWorkplace(model.WorkplaceId);
                Debit debit = new Debit()
                {
                    ItemId = item.Id,
                    Amount = model.Amount,
                    Date = model.Date,
                    WorkplaceId = workplace.Id,
                    Approved_By_Staff = staff,
                    Debited_By_Staff = staff,
                    DebitState = DebitState.Approved
                };
                debitList.Add(debit);
            }
            itemManager.BulkAddDebitItems(debitList);
            return View();
        }

        public ActionResult Delete(int id)
        {
            Debit debitToDelete = itemManager.GetDebit(id);
            var workplaceId = debitToDelete.WorkplaceId;
            itemManager.RemoveDebit(debitToDelete);
            return RedirectToAction("Details", "Workplace", new { id = workplaceId });
        }
    }
}