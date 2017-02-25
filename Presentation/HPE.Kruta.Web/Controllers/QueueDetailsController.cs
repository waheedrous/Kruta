using HPE.Kruta.Model;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HPE.Kruta.Web.Controllers
{
    [Authorize]
    public class QueueDetailsController : BaseController
    {
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplayQueueDetails(int queueID)
        {
            Queue queueModel = _queueManager.Get(queueID, true);
            return PartialView("_QueueDetailsPartial", queueModel);

            //JsonResult result = new JsonResult();
            //var serializer = new JavaScriptSerializer();

            //result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //result.Data = serializer.Serialize(queueModel);

            //return result;
        }
    }
}