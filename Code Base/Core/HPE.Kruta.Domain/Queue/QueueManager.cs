using HPE.Kruta.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPE.Kruta.Model;
using System.Data.Entity;

namespace HPE.Kruta.Domain
{
    public class QueueManager
    {
       public Queue GetQueueByIDWithRelatedData(int queueID)
        {
            Queue queue;
            using (var db = new ModelDBContext())
            {
                queue = db.Queues.Where(q => q.QueueID == queueID)
                    .Include(q => q.Document)
                    .Include(q => q.Property)
                    .Include(q => q.QueueStatus)
                    .Include(q => q.Document.DocumentSubType.DocumentType).First();
            }

            return queue;

        }

    }
}
