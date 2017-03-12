using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HPE.Kruta.Domain.User
{
    /// <summary>
    ///  handles the logic to retrieve and update data in the employee table
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// Get the employee by user name.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Employee GetByUsername(string username)
        {
            Employee emp = null;

            using (var db = new ModelDBContext())
            {
                emp = db.Employees.FirstOrDefault(u => string.Compare(u.UserName, username, System.StringComparison.OrdinalIgnoreCase) == 0);
            }

            return emp;
        }

        public Role GetRole(int? id)
        {
            Role role = null;

            using (var db = new ModelDBContext())
            {
                role = db.Roles.Find(id);
            }

            return role;
        }

        public EmployeeRole GetEmployeeRole(int? id)
        {
            EmployeeRole employeeRole = null;

            using (var db = new ModelDBContext())
            {
                employeeRole = db.EmployeeRoles.Find(id);
            }

            return employeeRole;
        }

        public IEnumerable<Role> ListRoles(bool includeDetails)
        {
            List<Role> roles;

            using (ModelDBContext db = new ModelDBContext())
            {
                if (includeDetails)
                {
                    roles = db.Roles
                    .Include(e => e.EmployeeRoles)
                    .ToList();
                }
                else
                {
                    roles = db.Roles.ToList();
                }
            }

            return roles;
        }

        /// <summary>
        /// lists all employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> ListEmployees()
        {
            List<Employee> employees;

            using (ModelDBContext db = new ModelDBContext())
            {
                employees = db.Employees.ToList();
            }

            return employees;
        }

        public void CreateEmployeeRole(EmployeeRole employeeRole)
        {
            using (var db = new ModelDBContext())
            {
                db.EmployeeRoles.Add(employeeRole);
                db.SaveChanges();
            }
        }

        public void AddRole(Role role)
        {
            using (ModelDBContext db = new ModelDBContext())
            {
                db.Roles.Add(role);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieve all the roles for specific user
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public List<EmployeeRole> ListEmployeeRoles(int employeeID)
        {
            List<EmployeeRole> employeeRoles;

            using (ModelDBContext db = new ModelDBContext())
            {
                employeeRoles = db.EmployeeRoles
                    .Include(q => q.Role)
                    .Where(q => q.EmployeeID == employeeID).ToList();
            }

            return employeeRoles;
        }

        public IEnumerable<EmployeeRole> ListEmployeeRoles()
        {
            List<EmployeeRole> employeeRoles;

            using (ModelDBContext db = new ModelDBContext())
            {
                employeeRoles = db.EmployeeRoles
                    .Include(q => q.Role)
                    .ToList();
            }

            return employeeRoles;
        }

        public void EditEmployeeRole(EmployeeRole employeeRole)
        {
            using (ModelDBContext db = new ModelDBContext())
            {
                db.Entry(employeeRole).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void EditRole(Role role)
        {
            using (ModelDBContext db = new ModelDBContext())
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void DeleteEmployeeRole(int id)
        {
            using (ModelDBContext db = new ModelDBContext())
            {
                EmployeeRole employeeRole = db.EmployeeRoles.Find(id);
                db.EmployeeRoles.Remove(employeeRole);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get the roles for specific user name.
        /// Use GetRolesForUser(int employeeID) for better performance.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string[] GetRolesForUser(string username)
        {
            Employee emp = GetByUsername(username);

            if (emp != null)
            {
                return GetRolesForUser(emp.EmployeeID);
            }

            return new string[] { };
        }

        public void DeleteRole(int id)
        {
            using (ModelDBContext db = new ModelDBContext())
            {
                Role role = db.Roles.Find(id);
                db.Roles.Remove(role);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Get the roles for specific user using the employee ID.
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public string[] GetRolesForUser(int employeeID)
        {
            List<EmployeeRole> employeeRoles = ListEmployeeRoles(employeeID);
            string[] rolesName = employeeRoles.Select(q => q.Role.RoleName).ToArray();
            return rolesName;
        }
    }
}
