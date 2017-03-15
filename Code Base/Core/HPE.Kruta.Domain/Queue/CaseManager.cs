using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HPE.Kruta.Domain
{
    public class CaseManager
    {
        public IEnumerable<PropertyCase> List()
        {
            List<PropertyCase> cases;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;

                cases = db.PropertyCases
                    .Include(c => c.Department)
                    .Include(c => c.Employee)
                    .Include(c => c.CaseType)
                    .ToList();
               
            }

            return cases;
        }

    }
}
