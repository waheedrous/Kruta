using HPE.Kruta.Domain.Property;
using HPE.Kruta.Domain.User;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Default action. Empty for now, just a redirection to the document queue page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {            
            return RedirectToAction("Index", "DocumentQueue");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}