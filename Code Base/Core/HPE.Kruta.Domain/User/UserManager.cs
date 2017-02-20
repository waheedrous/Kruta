using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Linq;

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

            using (var db = new ModelDBContext()) // use your DbConext
            {
                var emps = db.Employees.ToList()?.Where(u => u.UserName == username &&
                                                       password == System.Configuration.ConfigurationManager.AppSettings["GenericPassword"]);

                if (emps != null && emps.Count() > 0)
                {
                    emp = emps.First();
                }
            }

            if (emp != null)
            {
                return emp;
            }
            else
            {
                return null;
            }
        }
    }
}
