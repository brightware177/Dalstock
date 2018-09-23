using BL;
using BL.Managers;
using DAL.UnitOfWork;
using Dalstock_WebApp_Mysql_identity_19_02.Helpers;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Domain;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    public class ReportsController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;

        public ReportsController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            itemManager = new ItemManager(uow);
            workplaceManager = new WorkplaceManager(uow);
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Export(string GridHtml)
        {
            HtmlToPdf converter = new HtmlToPdf();

            // create a new pdf document converting an url
            
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString("http://dalstock.be/bobbin/details/6");

            // save pdf document
            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;
        }
    }
}