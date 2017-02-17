using HPE.Kruta.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Domain.User
{
    public class UserManager
    {
        public bool IsValid(string username, string password)
        {
            using (var db = new ModelDBContext()) // use your DbConext
            {
                return db.Employees.ToList().Any(u => u.UserNm == username && 
                                           password == System.Configuration.ConfigurationManager.AppSettings["GenericPassword"]);
            }
        }
    }
}
