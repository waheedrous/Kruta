﻿using HPE.Kruta.Domain;
using HPE.Kruta.Domain.Property;
using HPE.Kruta.Model;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
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
            try
            {

                _queueManager = new QueueManager();

                Queue queueModel = _queueManager.Get(queueID, true);

                DocumentManager documentManager = new DocumentManager();

                var documentStatuses = documentManager.ListDocumentStatus().OrderBy(o => o.Description);
                ViewBag.DocumentStatuses = new SelectList(documentStatuses, "DocumentStatusID", "Description", queueModel.Document.DocumentStatusID);

                var departments = new DepartmentManager().List(false).OrderBy(o => o.DepartmentName);
                ViewBag.DepartmentsList = new SelectList(departments, "DepartmentID", "DepartmentName");

                return PartialView("_QueueDetailsPartial", queueModel);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult SaveStatus(int queueID, int documentStatusID, string notes)
        {
            try
            {

                SaveStatusInternal(queueID, documentStatusID, notes);

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private void SaveStatusInternal(int queueID, int documentStatusID, string notes)
        {
            try
            {
                _queueManager = new QueueManager();
                _queueManager.UpdateQueueDocumentStatus(queueID, documentStatusID);
                SaveNotes(queueID, notes);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private void SaveNotes(int queueID, string notes)
        {

            if (!string.IsNullOrEmpty(notes))
            {
                _queueManager = new QueueManager();
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
            try
            {

                _queueManager = new QueueManager();
                if (queueID == 0 || departmentID == 0)
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

                SaveStatusInternal(queueID, documentStatusID, notes);

                this._queueManager.RouteQueue(queueID, departmentID);

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult AddNote(int queueID, string notes)
        {

            if (queueID == 0 || string.IsNullOrWhiteSpace(notes))
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            SaveNotes(queueID, notes);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);


        }
    }
}