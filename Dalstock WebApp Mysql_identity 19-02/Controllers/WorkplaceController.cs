using BL.Managers;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Helpers;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using Rotativa.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
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
            uow = new UnitOfWork(database, database);
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
            var workplace = workplaceManager.GetWorkplace(id);
            return View(workplace);
        }
        public ActionResult PrintViewToPdf(int id)
        {
            var workplace = workplaceManager.GetWorkplace(id);
            string filename = "Werf " + workplace.WorkplaceId + ".pdf";
            return new PartialViewAsPdf("_Details", workplace) { FileName = filename };
        }
    }
}