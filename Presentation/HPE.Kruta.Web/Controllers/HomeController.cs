using HPE.Kruta.Domain.Property;
using HPE.Kruta.Domain.User;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {   

            //Get Department name from DocumentManager and show into Dropdown List which is in index
            var department = new DepartmentManager().List(false).OrderBy(a => a.DepartmentName);
            ViewBag.DepartmentsList = new SelectList(department, "DepartmentID", "DepartmentName");
            LoadAssignToList();
            return View();
        }

        public void LoadAssignToList()
        {
            _userManager = new UserManager();
            var emps = _userManager.ListEmployees().OrderBy(o => o.EmployeeName);
            ViewBag.RoutingControlStaffList = new SelectList(emps, "EmployeeID", "EmployeeName");


        }

        public ActionResult Error()
        {
            return View();
        }
    }
}