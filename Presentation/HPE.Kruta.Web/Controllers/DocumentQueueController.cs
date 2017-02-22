using HPE.Kruta.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
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

        //[HttpGet]
        public ActionResult Queues_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Queue> queues = _queueManager.List(true).AsQueryable();
            DataSourceResult result = queues.ToDataSourceResult(request);


            var list = JsonConvert.SerializeObject(result, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(list, "application/json");

            //return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(result);
        }
    }
}