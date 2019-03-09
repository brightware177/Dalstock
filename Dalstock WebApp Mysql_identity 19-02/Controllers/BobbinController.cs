using BL;
using BL.Managers;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Helpers;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    [Authorize(Roles = "Admin,User,SuperUser")]
    public class BobbinController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;

        public BobbinController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            itemManager = new ItemManager(uow);
            workplaceManager = new WorkplaceManager(uow);
        }

        // GET: Bobbin
        public ActionResult Index()
        {
            var bobbins = itemManager.GetBobbins();
            return View(bobbins);
        }

        // GET: Bobbin/Details/5
        public ActionResult Details(int id)
        {
            BobbinDetailViewModel bdvm = new BobbinDetailViewModel();
            bdvm.Bobbin = itemManager.GetBobbin(id);
            bdvm.WorkplacesList = new List<SelectListItem>();
            foreach (var workplace in workplaceManager.GetWorkplaces().ToList())
            {
                bdvm.WorkplacesList.Add(new SelectListItem() { Text = workplace.WorkplaceId + " - " + workplace.Address + ", " + workplace.City.Name, Value = workplace.Id.ToString() });
            }
            bdvm.EmailViewModel = HelperClass.ReadMailSettings(Server.MapPath("~/App_Data/emailSettings.json"),"Dalcom");
            bdvm.EmailViewModel.Message = String.Format(bdvm.EmailViewModel.Message, bdvm.Bobbin.BobbinId);
            bdvm.EmailViewModel.Subject = String.Format(bdvm.EmailViewModel.Subject, bdvm.Bobbin.BobbinId);
            bdvm.EmailViewModel.id = id;
            bdvm.EmailViewModel.View = "Bobbin";
            return View(bdvm);
        }

        // GET: Bobbin/Create
        public ActionResult Create()
        {
            BobbinViewModel bvm = new BobbinViewModel();
            bvm.Infrastructures = workplaceManager.GetInfrastructures().ToList();
            bvm.CableTypes = itemManager.GetCableTypes().ToList();
            return View(bvm);
        }

        // POST: Bobbin/Create
        [HttpPost]
        public ActionResult Create(BobbinViewModel bvm)
        {
            if (ModelState.IsValid)
            {
                bvm.Bobbin.AmountRemains = bvm.Bobbin.CableLength;
                itemManager.AddBobbin(bvm.Bobbin);
                return RedirectToAction("Index", false);
            }
            bvm.CableTypes = itemManager.GetCableTypes().ToList();
            bvm.Infrastructures = workplaceManager.GetInfrastructures().ToList();
            return View(bvm);
        }

        // GET: Bobbin/Edit/5
        public ActionResult Edit(int id)
        {
            var bobbin = itemManager.GetBobbin(id);
            BobbinViewModel bvm = new BobbinViewModel();
            bvm.CableTypes = itemManager.GetCableTypes().ToList();
            bvm.Infrastructures = workplaceManager.GetInfrastructures().ToList();
            bvm.Bobbin = bobbin;
            return View(bvm);
        }

        // POST: Bobbin/Edit/5
        [HttpPost]
        public ActionResult Edit(BobbinViewModel bvm)
        {
            if (ModelState.IsValid)
            {
                itemManager.ChangeBobbin(bvm.Bobbin);
                return RedirectToAction("Index");
            }
            bvm.CableTypes = itemManager.GetCableTypes().ToList();
            bvm.Infrastructures = workplaceManager.GetInfrastructures().ToList();
            return View(bvm);
        }

        // GET: Bobbin/Delete/5
        public ActionResult Delete(int id)
        {
            itemManager.RemoveBobbin(id);
            return RedirectToAction("Index");
        }


        // GET: Bobbin/Delete/5
        public ActionResult DeleteBobbinDebit(int id)
        {
            var bobbin = itemManager.RemoveBobbinDebit(id);

            return RedirectToAction("Details", new { id = bobbin.Id });
        }


        public ActionResult RetourBobbin(int id)
        {
            var bobbin = itemManager.GetBobbin(id);
            bobbin.IsReturned = true;
            bobbin.ReturnDate = DateTime.Now;
            itemManager.ChangeBobbin(bobbin);
            return RedirectToAction("Details", new { id = id });
        }
        public ActionResult ContinueEditBobbin(int id)
        {
            var bobbin = itemManager.GetBobbin(id);
            bobbin.IsReturned = false;
            bobbin.ReturnDate = null;
            itemManager.ChangeBobbin(bobbin);
            return RedirectToAction("Details", new { id = id });
        }
        public ActionResult PrintViewToPdf(int id)
        {
            return new EmailController().GetPDF(id,"Bobbin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailViewModel emailViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                PartialViewAsPdf file = null;
                file = new EmailController().GetPDF(id, "Bobbin");
                var fileAsBytes = file.BuildPdf(this.ControllerContext);
                await new EmailController().SendEmail(emailViewModel, id, fileAsBytes, file.FileName);
                return PartialView("~/Views/Messages/_EmailSent.cshtml");
            }
            else
            {
                return PartialView("~/Views/Bobbin/_SendBobbinDetailMail.cshtml", emailViewModel);
            }
        }
    }
}
