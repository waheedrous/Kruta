using HPE.Kruta.Domain.Principals;
using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.Threading;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HPE.Kruta.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// MVC application start
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas(); 
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //read log4net configurations
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Assign the custom principal to the current context
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            Context.User = Thread.CurrentPrincipal = new KrutaPrincipal(User);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var raisedException = Server.GetLastError();
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            log.Error("Error in controller", raisedException);

        }

    }
}