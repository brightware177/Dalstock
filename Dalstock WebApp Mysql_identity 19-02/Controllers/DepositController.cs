using BL;
using BL.Managers;
using CsvHelper;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
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
                    Deposited_By_Staff = staff
                };
                depositList.Add(debit);
            }
            itemManager.BulkAddDepositItems(depositList);
            return View();
        }
        public ActionResult SmartDeposit()
        {
            return View();
        }
        [HttpPost]
        public string EditSmartDeposits(HttpPostedFileBase file)
        {
            var reader = new StreamReader(file.InputStream);
            ItemsImportViewModel import = new ItemsImportViewModel();
            var DepositList = new List<Deposit>();
            var FailedList = new List<Deposit>();
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.BadDataFound = null;
                var date = DateTime.Now;
                csv.Read();               
                date = csv.GetField<DateTime>(1);
                csv.ReadHeader();
                while (!csv.Parser.Context.HeaderRecord[0].Equals("Artikel"))
                {
                    csv.Read();csv.ReadHeader();
                }
                var userId = User.Identity.GetUserId();
                while (csv.Read())
                {
                    try
                    {
                        var x = csv.GetField<string>("Artikel").TrimStart(new Char[] { '0' });
                        if (!x.Equals(""))
                        {
                            var item = itemManager.GetItem(csv.GetField<string>("Artikel").TrimStart(new Char[] { '0' }));
                            var Deposit = new Deposit
                            {
                                ItemId = item.Id,
                                Item = item,
                                Amount = csv.GetField<int>("Aantal"),
                                Date = date,
                                Deposited_By_Staff = workplaceManager.GetStaffWithApplicationUserId(userId)
                            };
                            DepositList.Add(Deposit);
                        }
                    }
                    catch (KeyNotFoundException e)
                    {
                        var t = csv.GetField<int>("Artikel");
                        var x = int.Parse(csv.GetField<string>("Aantal").Replace(".", string.Empty));
                        var Deposit = new Deposit
                        {
                            ItemId = csv.GetField<int>("Artikel"),
                            Item = new Item() { Description = csv.GetField<string>("Omschrijving artikel"), ItemId = csv.GetField<string>("Artikel").TrimStart(new Char[] { '0' }) },
                            Amount = int.Parse(csv.GetField<string>("Aantal").Replace(".", string.Empty)),
                            Date = date,
                            Deposited_By_Staff = workplaceManager.GetStaffWithApplicationUserId(userId)
                        };
                        FailedList.Add(Deposit);
                    }
                    catch (CsvHelper.MissingFieldException e)
                    {

                    }
                }
            }
            import.ImportedItems = DepositList;
            import.FailedItems = FailedList;
            return JsonConvert.SerializeObject(import);
        }
        [HttpPost]
        public PartialViewResult SmartDepositsList(ItemsImportViewModel import)
        {
            try
            {
                import.ImportedItems = import.ImportedItems.OrderBy(x => x.ItemId).ToList();
            }
            catch (Exception)
            {

            }
            
            return PartialView("~/Views/Deposit/_EditSmartDeposits.cshtml", import);
        }
        [HttpPost]
        public ActionResult SaveImportedItems(ItemsImportViewModel import)
        {
            try
            {
                itemManager.BulkAddDepositItems(import.ImportedItems);
                return Json("Success");
            }
            catch (Exception)
            {
                return Json("Error!");
            }
            
        }
        [HttpPost]
        public ActionResult AddFailedItems(ItemsImportViewModel import)
        {
            try
            {
                foreach (var item in import.FailedItems)
                {
                    item.Item.Amount = item.Amount;
                }
                itemManager.BulkAddItems(import.FailedItems.Select(x=>x.Item));
                return Json("Success");
            }
            catch (Exception)
            {
                return Json("Error!");
            }
        }   
    }
}