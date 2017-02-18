using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Queues_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<vDocumentQueue> vdocumentqueues = db.vDocumentQueues;
            DataSourceResult result = vdocumentqueues.ToDataSourceResult(request);

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}