using HPE.Kruta.Domain;
using HPE.Kruta.Domain.Property;
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

            DocumentManager documentManager = new DocumentManager();

            var documentStatuses = documentManager.ListDocumentStatus(false).OrderBy(o => o.Description);
            ViewBag.DocumentStatuses = new SelectList(documentStatuses, "DocumentStatusID", "Description", queueModel.Document.DocumentStatusID);

            var departments = new DepartmentManager().List(false).OrderBy(o => o.DepartmentName);
            ViewBag.DepartmentsList = new SelectList(departments, "DepartmentID", "DepartmentName");

            return PartialView("_QueueDetailsPartial", queueModel);
        }

        [HttpGet]
        public ActionResult SaveStatus(int queueID, int documentStatusID, string notes )
        {
            SaveStatusInternal(queueID, documentStatusID, notes);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        private void SaveStatusInternal(int queueID, int documentStatusID, string notes)
        {
            _queueManager.UpdateQueueDocumentStatus(queueID, documentStatusID);
            SaveNotes(queueID, notes);
        }

        private void SaveNotes(int queueID, string notes)
        {
            if (!string.IsNullOrEmpty(notes))
            {
                QueueNoteManager queueNoteManager = new QueueNoteManager();
                queueNoteManager.Add(queueID, notes, LoggedInUserId);
            }
        }

        [HttpGet]
        public ActionResult GetDocumentPath(int documentID)
        {
            string documentPath = null;
            if (documentID > 0)
            {
                DocumentManager documentManager = new DocumentManager();
                documentPath = documentManager.GetDocumentPath(documentID);
            }
            return Json(new { DocumentPath = documentPath }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RouteQueueAndSave(int queueID, int departmentID, int documentStatusID, string notes)
        {
            if (queueID == 0 || departmentID == 0)
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            SaveStatusInternal(queueID, documentStatusID, notes);

            this._queueManager.RouteQueue(queueID, departmentID);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}