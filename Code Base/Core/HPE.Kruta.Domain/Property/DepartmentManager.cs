using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Linq;

namespace HPE.Kruta.Domain.Property
{
    public class DepartmentManager
    {
        public List<Department> List(bool includeDetails)
        {
            List<Department> departments;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;
                //db.Configuration.LazyLoadingEnabled = false;
                if (includeDetails)
                {
                    departments = db.Departments
                        //.Include(q => TODO)
                        //.Include(q => q.Document.DocumentStatus)
                        //.Include(q => q.Document.DocumentSubType.DocumentType)
                        //.Include(q => q.Property)
                        //.Include(q => q.QueueStatus)
                        //.Include(q => q.Department)
                        //.Include(q => q.Employee)
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
