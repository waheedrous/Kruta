using HPE.Kruta.Domain;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        public ActionResult Index()
        {

            QueueNoteManager m1 = new QueueNoteManager();

            m1.Add(13, "test from the app");


            QueueManager m = new QueueManager();

            var q = m.Get(12, true);

            //string a = q.Property.ParcelNumber;
            //string a1 = q.Document.DocumentSubType.DocumentType.Description;

            return View();
        }
    }
}