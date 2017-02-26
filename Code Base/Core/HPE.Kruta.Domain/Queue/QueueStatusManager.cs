using System.Collections.Generic;
using HPE.Kruta.Model;
using HPE.Kruta.DataAccess;
using System.Linq;

namespace HPE.Kruta.Domain
{
    public class QueueStatusManager
    {
        public List<QueueStatus> List()
        {
            using (var db = new ModelDBContext())
            {
                return db.QueueStatus.ToList();
            }

        }


    }
}
