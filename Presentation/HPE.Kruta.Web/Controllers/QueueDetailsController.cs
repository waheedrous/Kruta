using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class QueueDetailsController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }
    }
}