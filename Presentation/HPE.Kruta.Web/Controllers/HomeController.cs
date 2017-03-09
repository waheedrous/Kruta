using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            LoadAssignToList();
            return View();
        }

        public void LoadAssignToList()
        {
            _userManager = new Domain.User.UserManager();
            var emps = _userManager.List().OrderBy(o => o.EmployeeName);
            ViewBag.RoutingControlStaffList = new SelectList(emps, "EmployeeID", "EmployeeName");


        }
    }
}