using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GA.BDC.Web.Scratchcard.MVC.Startup))]
namespace GA.BDC.Web.Scratchcard.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
