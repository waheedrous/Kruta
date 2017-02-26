using HPE.Kruta.Domain;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Linq;
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


            QueueStatusManager statusManager = new QueueStatusManager();

            IEnumerable<SelectListItem> statusList = from s in statusManager.List()
                                                     select new SelectListItem
                                                     {
                                                         Selected = queueModel.QueueStatusID == s.QueueStatusID,
                                                         Text = s.Description,
                                                         Value = s.QueueStatusID.ToString()
                                                     };

            ViewBag.QueueStatuses = statusList;



            return PartialView("_QueueDetailsPartial", queueModel);

        }


        [HttpGet]
        public ActionResult SaveStatus(int queueID, int queueStatusID, string notes )
        {
            _queueManager.UpdateQueueStatus(queueID, queueStatusID);

            if (!string.IsNullOrEmpty(notes))
            {
                QueueNoteManager queueNoteManager = new QueueNoteManager();
                queueNoteManager.Add(queueID, notes, LoggedInUserId);
            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetDocumentPath(string documentID)
        {
            string documentPath = null;
            if (!string.IsNullOrWhiteSpace(documentID))
            {
                DocumentManager documentManager = new DocumentManager();
                documentPath = documentManager.GetDocumentPath(int.Parse(documentID));
            }
            return Json(new { DocumentPath = documentPath }, JsonRequestBehavior.AllowGet);
        }
    }
}