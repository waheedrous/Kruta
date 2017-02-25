using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Linq;
using System;
using System.Collections.Generic;

namespace HPE.Kruta.Domain.User
{
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

        public List<Employee> List()
        {
            List<Employee> employees;

            using (var db = new ModelDBContext())
            {
                employees = db.Employees.ToList();
            }

            return employees;
        }
    }
}
