using HPE.Kruta.Domain;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            QueueManager m = new QueueManager();

            var q = m.Get(12, true);

            string a = q.Property.ParcelNumber;
            string a1 = q.Document.DocumentSubType.DocumentType.Description;

            return View();
        }
    }
}