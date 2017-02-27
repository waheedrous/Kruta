using HPE.Kruta.Model;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    [Authorize]
    public class DocumentQueueController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Queues_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Queue> queues = this._queueManager.List(true).AsQueryable();
            DataSourceResult result = queues.ToDataSourceResult(request);

            var list = JsonConvert.SerializeObject(result, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                });

            return Content(list, "application/json");
        }

        [HttpGet]
        public ActionResult Queues_BatchAssign(List<int> selectedQueueIds, int empId)
        {
            if (selectedQueueIds == null || selectedQueueIds.Count == 0 || empId == 0)
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            this._queueManager.AssignEmployeeBulk(selectedQueueIds, empId);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}