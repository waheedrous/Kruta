using HPE.Kruta.Common.Enum;
using HPE.Kruta.Domain;
using HPE.Kruta.Domain.Property;
using HPE.Kruta.Domain.User;
using HPE.Kruta.Model.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    
    public class DocumentQueueController : BaseController
    {
        /// <summary>
        /// Default action. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //Get Department name from DocumentManager and show into Drop down List which is in index
            var department = new DepartmentManager().List(false).OrderBy(a => a.DepartmentName);
            ViewBag.DepartmentsList = new SelectList(department, "DepartmentID", "DepartmentName");

            var caseTypes = new CaseTypeManager().List().OrderBy(a => a.Name);
            ViewBag.CaseTypesList = new SelectList(caseTypes, "CaseTypeID", "Name");

            LoadAssignToList();
            return View();
        }

        /// <summary>
        /// get unprocessed queue items
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult Queues_Read([DataSourceRequest]DataSourceRequest request)
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

        /// <summary>
        /// assign a bulk of documents to a single employee
        /// </summary>
        /// <param name="selectedQueueIds">list of selected queues ids</param>
        /// <param name="empId">employee id to assign to</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Queues_BatchAssign(List<int> selectedQueueIds, int empId)
        {
                _queueManager = new QueueManager();

                if (selectedQueueIds == null || selectedQueueIds.Count == 0 || empId == 0)
                    return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

                this._queueManager.AssignEmployeeBulk(selectedQueueIds, empId);

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Load the assign to drop down list
        /// </summary>
        private void LoadAssignToList()
        {
            _userManager = new UserManager();
            var emps = _userManager.ListEmployees().OrderBy(o => o.EmployeeName);
            ViewBag.RoutingControlStaffList = new SelectList(emps, "EmployeeID", "EmployeeName");
        }

        /// <summary>
        /// routes a queue item and saves history
        /// </summary>
        /// <param name="selectedQueueIds"></param>
        /// <param name="departmentID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RouteQueueAndSave(List<int> selectedQueueIds, int departmentID)
        {

            _queueManager = new QueueManager();
            if (selectedQueueIds == null || selectedQueueIds.Count == 0 || departmentID == 0)
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            this._queueManager.RouteQueueList(selectedQueueIds, departmentID);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }
    }
}