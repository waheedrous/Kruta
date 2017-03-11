using HPE.Kruta.Model;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HPE.Kruta.Web.Controllers
{
    public class EmployeeRolesController : BaseController
    {
        public EmployeeRolesController()
        {
            _userManager = new Domain.User.UserManager();
        }

        // GET: EmployeeRoles
        public ActionResult Index()
        {
            var employeeRoles = _userManager.ListEmployeeRoles();
            return View(employeeRoles.ToList());
        }

        // GET: EmployeeRoles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRole employeeRole = _userManager.GetEmployeeRole(id);
                
            if (employeeRole == null)
            {
                return HttpNotFound();
            }
            return View(employeeRole);
        }

        // GET: EmployeeRoles/Create
        public ActionResult Create()
        {
            var roles = _userManager.ListRoles();
            ViewBag.RoleID = new SelectList(roles, "RoleID", "RoleName");
            return View();
        }

        // POST: EmployeeRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeRoleID,EmployeeID,RoleID")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                _userManager.CreateEmployeeRole(employeeRole);
                
                return RedirectToAction("Index");
            }

            var roles = _userManager.ListRoles();
            ViewBag.RoleID = new SelectList(roles, "RoleID", "RoleName", employeeRole.RoleID);
            return View(employeeRole);
        }

        // GET: EmployeeRoles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRole employeeRole = _userManager.GetEmployeeRole(id);
            if (employeeRole == null)
            {
                return HttpNotFound();
            }
            var roles = _userManager.ListRoles();
            ViewBag.RoleID = new SelectList(roles, "RoleID", "RoleName", employeeRole.RoleID);
            return View(employeeRole);
        }

        // POST: EmployeeRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeRoleID,EmployeeID,RoleID")] EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                _userManager.EditEmployeeRole(employeeRole);
                
                return RedirectToAction("Index");
            }
            var roles = _userManager.ListRoles();
            ViewBag.RoleID = new SelectList(roles, "RoleID", "RoleName", employeeRole.RoleID);
            return View(employeeRole);
        }

        // GET: EmployeeRoles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeRole employeeRole = _userManager.GetEmployeeRole(id);
            if (employeeRole == null)
            {
                return HttpNotFound();
            }
            return View(employeeRole);
        }

        // POST: EmployeeRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userManager.DeleteEmployeeRole(id);
            
            return RedirectToAction("Index");
        }
    }
}
