using HPE.Kruta.Model;
using System.Linq;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class EmployeeRolesController : BaseController
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EmployeeRolesController()
        {
            _userManager = new Domain.User.UserManager();
        }

        /// <summary>
        /// Default method. Loads all roles and employee list.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var roles = _userManager.ListRoles(true);

            var emps = _userManager.ListEmployees().OrderBy(o => o.EmployeeName);
            ViewBag.EmployeeList = new SelectList(emps, "EmployeeID", "EmployeeName");

            ViewBag.EmployeeID = emps.First().EmployeeID;

            return View(roles.ToList());
        }

        /// <summary>
        /// Load the roles for the selected employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult LoadEmployeeRoles(int? id)
        {
            var roles = _userManager.ListRoles(true);
            ViewBag.EmployeeID = id;
            return PartialView("_EmployeeRolesPartial", roles.ToList());
        }

        /// <summary>
        /// Handle Employee Role Selection to add or remove the role from the employee
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="employeeId"></param>
        /// <param name="isSelected"></param>
        /// <param name="employeeRoleId"></param>
        public void HandleEmployeeRoleSelection(int roleId, int employeeId, bool isSelected, int employeeRoleId)
        {
            if (isSelected)
            {
                // this is an insert operation
                EmployeeRole employeeRole = new EmployeeRole { RoleID = roleId, EmployeeID = employeeId };
                _userManager.CreateEmployeeRole(employeeRole);
            }
            else
            {
                // this is a delete operation
                if (employeeRoleId > 0)
                {
                    _userManager.DeleteEmployeeRole(employeeRoleId);
                }
            }
            Session["IsAuthorized"] = null;
        }
    }
}
