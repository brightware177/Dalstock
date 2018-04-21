using DAL;
using Dalstock_WebApp_Mysql_identity_19_02.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        
        public ActionResult Index()
        {
            
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var user = UserManager.FindById(User.Identity.GetUserId());
            string database = user.Database;
            string username = user.Database;
            var ctx = new ItemDbContext(database, username);
            var list = ctx.Workplaces.Include("Debits").ToList();
            var list2 = ctx.Staff.Include("Debited_debits").Include("Approved_debits").ToList();
            var list3 = ctx.Debits.Include("Workplace").Include("Item").Include("Debited_By_Staff").ToList();
            var list4 = ctx.Debits.Include("Workplace").Include("Item").Include("Debited_By_Staff").Where(a => a.DebitState == Domain.DebitState.Approved).ToList();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}