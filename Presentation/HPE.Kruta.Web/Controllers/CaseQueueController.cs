using HPE.Kruta.Common.Enums;
using HPE.Kruta.Domain;
using HPE.Kruta.Domain.Property;
using HPE.Kruta.Domain.User;
using HPE.Kruta.Model;
using HPE.Kruta.Model.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{

    public class CaseQueueController : BaseController
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
            LoadAssignToList();
            return View();
        }

        /// <summary>
        /// get unprocessed queue items
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ActionResult CaseQueue_Read([DataSourceRequest]DataSourceRequest request)
        {
            PropertyCaseManager caseManager = new PropertyCaseManager();

            IQueryable<PropertyCase> queues = caseManager.List().AsQueryable();
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
        public ActionResult CaseQueues_BatchAssign(List<int> selectedQueueIds, int empId)
        {
            PropertyCaseManager caseManager = new PropertyCaseManager();

            if (selectedQueueIds == null || selectedQueueIds.Count == 0 || empId == 0)
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            caseManager.AssignEmployeeBulk(selectedQueueIds, empId);

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult RouteQueueAndSave(List<int> selectedQueueIds, int departmentID)
        {
            PropertyCaseManager caseManager = new PropertyCaseManager();
            if (selectedQueueIds == null || selectedQueueIds.Count == 0 || departmentID == 0)
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);

            caseManager.RouteQueueList(selectedQueueIds, departmentID);

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
    }
}