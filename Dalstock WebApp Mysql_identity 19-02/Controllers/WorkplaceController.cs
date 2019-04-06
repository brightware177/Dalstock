using BL.Managers;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Helpers;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    [Authorize]
    public class WorkplaceController : Controller
    {
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;

        public WorkplaceController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            workplaceManager = new WorkplaceManager(uow);
        }
        // GET: Workplace
        public ActionResult Index()
        {
            var workplaces = workplaceManager.GetWorkplaces().ToList();
            return View(workplaces);
        }
        public ActionResult Create()
        {
            var cities = workplaceManager.GetCities().Select(s => new
            {
                Text = s.Zipcode + " " + s.Name,
                Value = s.CityId
            }).ToList();
            ViewBag.Cities = new SelectList(cities, "Value", "Text");
            var infras = workplaceManager.GetInfrastructures().Select(s => new
            {
                Text = s.Description,
                Value = s.InfrastructureId
            }).ToList();
            ViewBag.Infras = new SelectList(infras, "Value", "Text");
            return View();
        }
        // POST: Workplace/Create
        [HttpPost]
        public ActionResult Create(Workplace model)
        {
            if (ModelState.IsValid)
            {
                model.Address = HelperClass.FirstCharToUpper(model.Address);
                workplaceManager.AddWorkplace(model);
                return RedirectToAction("Index");
            }
            else
            {
                var cities = workplaceManager.GetCities().Select(s => new
                {
                    Text = s.Zipcode + " " + s.Name,
                    Value = s.CityId
                }).ToList();
                ViewBag.Cities = new SelectList(cities, "Value", "Text");
                var infras = workplaceManager.GetInfrastructures().Select(s => new
                {
                    Text = s.Description,
                    Value = s.InfrastructureId
                }).ToList();
                ViewBag.Infras = new SelectList(infras, "Value", "Text");
                return View(model);
            }
        }
        // GET: Items/Edit/5
        public ActionResult Edit(int id)
        {
            var cities = workplaceManager.GetCities().Select(s => new
            {
                Text = s.Zipcode + " " + s.Name,
                Value = s.CityId
            }).ToList();
            ViewBag.Cities = new SelectList(cities, "Value", "Text");
            var infras = workplaceManager.GetInfrastructures().Select(s => new
            {
                Text = s.Description,
                Value = s.InfrastructureId
            }).ToList();
            ViewBag.Infras = new SelectList(infras, "Value", "Text");
            var itemToEdit = workplaceManager.GetWorkplace(id);
            return View(itemToEdit);
        }

        // POST: Items/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Workplace model)
        {
            if (ModelState.IsValid)
            {
                model.Address = HelperClass.FirstCharToUpper(model.Address);
                workplaceManager.ChangeWorkplace(model);
                return RedirectToAction("Index");
            }
            else
            {
                var cities = workplaceManager.GetCities().Select(s => new
                {
                    Text = s.Zipcode + " " + s.Name,
                    Value = s.CityId
                }).ToList();
                ViewBag.Cities = new SelectList(cities, "Value", "Text");
                var infras = workplaceManager.GetInfrastructures().Select(s => new
                {
                    Text = s.Description,
                    Value = s.InfrastructureId
                }).ToList();
                ViewBag.Infras = new SelectList(infras, "Value", "Text");
                return View(model);
            }
        }
        public ActionResult Delete(int id)
        {
            var workplace = workplaceManager.GetWorkplace(id);
            workplaceManager.RemoveWorkplace(workplace);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            WorkplaceDetailViewModel wdvm = new WorkplaceDetailViewModel();
            wdvm.Workplace = workplaceManager.GetWorkplace(id);
            wdvm.EmailViewModel = HelperClass.ReadMailSettings(Server.MapPath("~/App_Data/emailSettings.json"), "Dalcom");
            wdvm.EmailViewModel.Message = String.Format(wdvm.EmailViewModel.Message, wdvm.Workplace.WorkplaceId);
            wdvm.EmailViewModel.Subject = String.Format(wdvm.EmailViewModel.Subject, wdvm.Workplace.WorkplaceId);
            wdvm.EmailViewModel.id = id;
            wdvm.EmailViewModel.View = "Workplace";
            return View(wdvm);
        }
        public ActionResult PrintViewToPdf(int id, bool printItems)
        {
            TempData["printItems"] = printItems;
            return new EmailController().GetPDF(id,"Workplace");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailViewModel emailViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                var file = new EmailController().GetPDF(id, "Workplace");
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