using HPE.Kruta.Domain;
using HPE.Kruta.Domain.Principals;
using HPE.Kruta.Domain.User;
using log4net;
using log4net.Config;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    [KrutaAuthorize]
    public class BaseController : Controller
    {
        //public ObjectCache _cache = MemoryCache.Default;
        public QueueManager _queueManager;
        public UserManager _userManager;



        public int LoggedInUserId
        {
            get
            {
                return Convert.ToInt32(Session["UserID"]);
            }
        }

        /// <summary>
        /// this method will be triggered with each exception in the controllers
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            filterContext.ExceptionHandled = true;

            log.Error("Error in controller", filterContext.Exception);
            
        }
    }
}