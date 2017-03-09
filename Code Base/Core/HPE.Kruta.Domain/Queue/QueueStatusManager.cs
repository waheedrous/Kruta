using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Linq;

namespace HPE.Kruta.Domain
{
    /// <summary>
    ///  handles the logic to retrieve and update data in the Queue status table
    /// </summary>
    public class QueueStatusManager
    {
        /// <summary>
        /// get all statuses
        /// </summary>
        /// <returns></returns>
        public List<QueueStatus> List()
        {
            using (var db = new ModelDBContext())
            {
                return db.QueueStatus.ToList();
            }

        }


    }
}
