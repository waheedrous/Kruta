using HPE.Kruta.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class DocumentQueueController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Queues_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Queue> queues = _queueManager.List(true).AsQueryable();
            DataSourceResult result = queues.ToDataSourceResult(request);

            return Json(result);
        }
    }
}