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
using System.IO;
using CsvHelper;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

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

        public ActionResult SmartDebit()
        {
            return View();
        }
        [HttpPost]
        public string EditSmartDebits(HttpPostedFileBase file)
        {
            var reader = new StreamReader(file.InputStream);
            ItemsDebitImportViewModel import = new ItemsDebitImportViewModel();
            var DebitList = new List<Debit>();
            var FailedList = new List<Debit>();
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.BadDataFound = null;

                csv.Read();
                csv.ReadHeader();
                while (!csv.Parser.Context.HeaderRecord[0].Equals("Magazijn"))
                {
                    csv.Read(); csv.ReadHeader();
                }
                var staff = workplaceManager.GetStaffWithApplicationUserId(User.Identity.GetUserId());
                Workplace workplace = new Workplace() { WorkplaceId = "0" };
                var itemsList = itemManager.GetItems();

                while (csv.Read())
                {
                    try
                    {
                        if (!csv.GetField<string>("Order/Netwerk").Equals(""))
                        {
                            if (!workplace.WorkplaceId.Equals(csv.GetField<string>("Order/Netwerk").TrimStart(new Char[] { '0' })))
                            {
                                workplace = workplaceManager.GetWorkplace(csv.GetField<string>("Order/Netwerk").TrimStart(new Char[] { '0' }));
                            }
                            var item = itemsList.Single(x => x.ItemId.Equals(csv.GetField<string>("Artikel").TrimStart(new Char[] { '0' })));
                            var Debit = new Debit
                            {
                                ItemId = item.Id,
                                WorkplaceId = workplace.Id,
                                Item = item,
                                Amount = int.Parse(csv.GetField<string>("Hoeveelheid").Replace(",000", "")),
                                Date = csv.GetField<DateTime>("Boekingsdatum"),
                                Debited_By_Staff = staff,
                                Approved_By_Staff = staff,
                                Workplace = workplace,
                                DebitState = DebitState.Approved
                            };
                            DebitList.Add(Debit);
                        }
                    }
                    catch (KeyNotFoundException e)
                    {
                        var x = int.Parse(csv.GetField<string>("Hoeveelheid").Replace(",000", ""));
                        var Debit = new Debit
                        {
                            ItemId = csv.GetField<int>("Artikel"),
                            Item = new Item() { Description = csv.GetField<string>("Artikelomschrijving"), ItemId = csv.GetField<string>("Artikel").TrimStart(new Char[] { '0' }) },
                            Amount = int.Parse(csv.GetField<string>("Hoeveelheid").Replace(",000", "")),
                            Date = csv.GetField<DateTime>("Boekingsdatum"),
                            Debited_By_Staff = staff,
                            Workplace = new Workplace() { WorkplaceId = csv.GetField<string>("Order/Netwerk").TrimStart(new Char[] { '0' }) }
                        };
                        FailedList.Add(Debit);
                    }
                    catch (CsvHelper.MissingFieldException e)
                    {

                    }
                }
            }
            import.ImportedItems = DebitList;
            import.FailedItems = FailedList;
            return JsonConvert.SerializeObject(import);
        }
        [HttpPost]
        public PartialViewResult SmartDebitsList(ItemsDebitImportViewModel import)
        {
            try
            {
                import.ImportedItems = import.ImportedItems.OrderBy(x => x.WorkplaceId).ToList();
            }
            catch (Exception)
            {

            }

            return PartialView("~/Views/Debit/_EditSmartDebit.cshtml", import);
        }
        [HttpPost]
        public ActionResult SaveImportedItems(ItemsDebitImportViewModel import)
        {
            try
            {
                var staff = workplaceManager.GetStaffWithApplicationUserId(User.Identity.GetUserId());
                foreach (var item in import.ImportedItems)
                {
                    item.Debited_By_Staff_Id = staff.StaffId;
                    item.Approved_By_Staff_Id = staff.StaffId;
                }
                itemManager.BulkAddDebitItems(import.ImportedItems);
                return Json("Success");
            }
            catch (Exception e)
            {
                return Json("Error!");
            }
         }
        [HttpPost]
        public ActionResult AddFailedItems(ItemsImportViewModel import)
        {
            try
            {
                itemManager.BulkAddItems(import.FailedItems.Select(x => x.Item));
                return Json("Success");
            }
            catch (Exception)
            {
                return Json("Error!");
            }
        }
    }
}