using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HPE.Kruta.Domain
{
    public class CaseManager
    {
        public List<Case> List()
        {
            List<Case> cases;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;

                cases = db.Cases
                    .Include(c => c.Department)
                    .Include(c => c.AssignedToEmployee)
                    .Include(c => c.CaseType)
                    .ToList();
               
            }

            return cases;
        }

    }
}
