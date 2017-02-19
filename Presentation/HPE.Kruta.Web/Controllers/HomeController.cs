using HPE.Kruta.Domain;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            QueueManager m = new QueueManager();

            var q = m.GetQueueByIDWithRelatedData(12);

            string a = q.Property.ParcelNumber;
            string a1 = q.Document.DocumentSubType.DocumentType.Description;

            return View();
        }
    }
}