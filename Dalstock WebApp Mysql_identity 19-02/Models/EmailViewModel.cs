using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Dalstock_WebApp_Mysql_identity_19_02.Models
{
    public class EmailViewModel
    {
        [Required, Display(Name = "Onderwerp")]
        public string Subject { get; set; }
        [Display(Name = "Cc")]
        public string CC { get; set; }
        [Required, Display(Name = "Geaddresseerden"), EmailAddress]
        public string ToMails { get; set; }
        [Required, Display(Name = "Bericht")]
        public string Message { get; set; }
        public string Company { get; set; }
        public int id { get; set; }
        public string View { get; set; }
    }
}