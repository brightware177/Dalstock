using System.Web;
using System.Web.Mvc;

namespace Dalstock_WebApp_Mysql_identity_19_02
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
