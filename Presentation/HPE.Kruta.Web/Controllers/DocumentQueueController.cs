using HPE.Kruta.Common.Enum;
using HPE.Kruta.Model.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HPE.Kruta.Domain;

namespace HPE.Kruta.Web.Controllers
{
    //[Authorize]
    public class DocumentQueueController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Queues_Read([DataSourceRequest]DataSourceRequest request)
        {

            try
            {
                _queueManager = new QueueManager();

                IQueryable<QueueWithSequence> queues = this._queueManager.ListWithSequence().AsQueryable();
                DataSourceResult result = queues.ToDataSourceResult(request);

                var list = JsonConvert.SerializeObject(result, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    });

                return Content(list, "application/json");

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        //[AuthorizePermission(RolesEnum.CanAssign)]
        public ActionResult Queues_BatchAssign(List<int> selectedQueueIds, int empId)
        {
            try
            {
                _queueManager = new QueueManager();

                if (selectedQueueIds == null || selectedQueueIds.Count == 0 || empId == 0)
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

                this._queueManager.AssignEmployeeBulk(selectedQueueIds, empId);

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}