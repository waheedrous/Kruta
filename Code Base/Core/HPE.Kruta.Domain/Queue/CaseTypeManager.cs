using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Domain
{
    public class CaseTypeManager
    {
        public List<CaseType> List()
        {
            List<CaseType> caseTypes;
            using (var db = new ModelDBContext())
            {
                caseTypes = db.CaseTypes
                    .ToList();
            }

            return caseTypes;
        }
    }
}
