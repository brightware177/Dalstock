using BL;
using BL.Managers;
using DAL.UnitOfWork;
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
    public class EmailController : Controller
    {
        ItemManagerService itemManager;
        WorkplaceManagerService workplaceManager;
        IUnitOfWork uow;

        public EmailController()
        {
            var database = System.Web.HttpContext.Current.User.Identity.GetDatabase();
            var databaseUsername = System.Web.HttpContext.Current.User.Identity.GetDatabaseUsername();
            uow = new UnitOfWork(databaseUsername, database);
            itemManager = new ItemManager(uow);
            workplaceManager = new WorkplaceManager(uow);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task SendEmail(EmailViewModel emailViewModel, int id, byte[] fileBytes, string fileName)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(emailViewModel.ToMails));
            message.Subject = emailViewModel.Subject;
            message.Body = emailViewModel.Message;
            message.Attachments.Add(new Attachment(new MemoryStream(fileBytes), fileName));
            message.IsBodyHtml = true;
            message.CC.Add(new MailAddress(emailViewModel.CC));
                using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
            }
        }
        public PartialViewAsPdf GetPDF(int id, string type)
        {
            if (type.Equals("Bobbin"))
            {
                BobbinDetailViewModel bdvm = new BobbinDetailViewModel();
                bdvm.Bobbin = itemManager.GetBobbin(id);
                bdvm.WorkplacesList = new List<SelectListItem>();
                foreach (var workplace in workplaceManager.GetWorkplaces().ToList())
                {
                    bdvm.WorkplacesList.Add(new SelectListItem() { Text = workplace.WorkplaceId + " - " + workplace.Address + ", " + 
                        workplace.City.Name, Value = workplace.Id.ToString() });
                }
                string filename = bdvm.Bobbin.BobbinId + " - bobijn.pdf";
                return new PartialViewAsPdf("_Details", bdvm) { FileName = filename };
            }
            else
            {
                var workplace = workplaceManager.GetWorkplace(id);
                string filename = "Werf " + workplace.WorkplaceId + ".pdf";
                return new PartialViewAsPdf("_Details", workplace) { FileName = filename };
            }
        }
    }
}