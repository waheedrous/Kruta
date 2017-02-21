using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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

        public int Update(Queue queue)
        {
            using (var db = new ModelDBContext())
            {
                db.SaveChanges();

            }

            return queue.QueueID;
        }

        public int RouteQueue(Queue queue)
        {
            using (var db = new ModelDBContext())
            {

                var currentQueue = db.Queues.Where(q => q.QueueID == queue.QueueID).First();

                db.SaveChanges();

            }

            return queue.QueueID;
        }

    }

    
}
