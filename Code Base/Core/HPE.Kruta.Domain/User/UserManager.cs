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
        /// checks if he user login is valid
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValid(string username, string password)
        {
            Employee emp = VerifyUser(username, password);

            return emp != null;
        }

        public Employee VerifyUser(string username, string password)
        {
            Employee emp = null;
            // This should be temp solution and it may be replaced with an actual database driven password in the future
            string genericPassword = System.Configuration.ConfigurationManager.AppSettings["GenericPassword"];

            using (var db = new ModelDBContext())
            {
                emp = db.Employees.FirstOrDefault(u => u.UserName == username &&
                                              password == genericPassword);
            }

            // It will be null if the username/password are wrong, the check for null should happen on the caller side
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
        /// Get the roles for specific user id
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
