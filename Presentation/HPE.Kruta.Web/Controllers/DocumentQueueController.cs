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

        //[HttpGet]
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

            //return Json(result, JsonRequestBehavior.AllowGet);
            //return Json(result);
        }

        [HttpGet]
        public ActionResult Queues_BatchAssign(string selectedQueueIds, string empId)
        {
            if (string.IsNullOrWhiteSpace(selectedQueueIds) || string.IsNullOrWhiteSpace(empId))
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            List<int> queueIds = selectedQueueIds.Split(',').Select(int.Parse).ToList();

            this._queueManager.AssignEmployeeBulk(queueIds, int.Parse(empId));

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}