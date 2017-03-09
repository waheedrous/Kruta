using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Linq;

namespace HPE.Kruta.Domain.Property
{
    /// <summary>
    /// handles the logic to retrieve and update data in the department table
    /// </summary>
    public class DepartmentManager
    {
        /// <summary>
        /// returns a list of all departments
        /// </summary>
        /// <param name="includeDetails">has no effect, should be removed</param>
        /// <returns></returns>
        public List<Department> List(bool includeDetails)
        {
            List<Department> departments;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                if (includeDetails)
                {
                    departments = db.Departments
                        .ToList();
                }
                else
                {
                    departments = db.Departments.ToList();
                }
            }

            return departments;
        }
    }
}
