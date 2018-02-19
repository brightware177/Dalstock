using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dalstock_WebApp_Mysql_identity_19_02.Startup))]
namespace Dalstock_WebApp_Mysql_identity_19_02
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
