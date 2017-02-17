using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HPE.Kruta.Web.Startup))]
namespace HPE.Kruta.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
