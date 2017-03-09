using HPE.Kruta.Common.Enum;
using HPE.Kruta.Domain;
using HPE.Kruta.Domain.Principals;
using HPE.Kruta.Domain.User;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    //[KrutaAuthorize]
    public class BaseController : Controller
    {
        //public ObjectCache _cache = MemoryCache.Default;
        public QueueManager _queueManager;
        public UserManager _userManager;

        public int LoggedInUserId
        {
            get
            {
                var user = User as KrutaPrincipal;
                int userId = 0;

                if (user != null)
                {
                    userId = user.UserID;
                }

                return userId;
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            //filterContext.ExceptionHandled = true;

            RedirectToAction("Error", "Home");

        }
    }
}