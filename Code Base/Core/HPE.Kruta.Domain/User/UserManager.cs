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
        public bool IsValid(string username, string password)
        {
            Employee emp = VerifyUser(username, password);

            return emp != null;
        }

        public Employee VerifyUser(string username, string password)
        {
            Employee emp = null;

            using (var db = new ModelDBContext())
            {
                emp = db.Employees.FirstOrDefault(u => string.Compare(u.UserName, username, System.StringComparison.OrdinalIgnoreCase) == 0);
            }

            return emp;
        }

        /// <summary>
        /// lists all employees
        /// </summary>
        /// <returns></returns>
        public List<Employee> List()
        {
            List<Employee> employees;

            using (ModelDBContext db = new ModelDBContext())
            {
                    employees = db.Employees.ToList();
            }

            return employees;
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
