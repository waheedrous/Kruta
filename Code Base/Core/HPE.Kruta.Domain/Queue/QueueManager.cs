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
        public Queue Get(int queueID, bool includeDetails)
        {
            Queue queue;
            using (var db = new ModelDBContext())
            {
                if (includeDetails)
                {
                    queue = db.Queues.Where(q => q.QueueID == queueID)
                        .Include(q => q.Document)
                        .Include(q => q.Property)
                        .Include(q => q.QueueStatus)
                        .Include(q => q.Document.DocumentSubType.DocumentType)
                        .Include(q => q.QueueNotes)
                        .First();

                }
                else
                {
                    queue = db.Queues.Where(q => q.QueueID == queueID).First();
                }
            }

            return queue;

        }


        public List<Queue> List(bool includeDetails)
        {

            List<Queue> queues;
            using (var db = new ModelDBContext())
            {
                if (includeDetails)
                {
                    queues = db.Queues
                        .Include(q => q.Document)
                        .Include(q => q.Property)
                        .Include(q => q.QueueStatus)
                        .Include(q => q.Document.DocumentSubType.DocumentType)
                        .ToList();

                }
                else
                {
                    queues = db.Queues.ToList();
                }
            }

            return queues;
        }
    }
}
