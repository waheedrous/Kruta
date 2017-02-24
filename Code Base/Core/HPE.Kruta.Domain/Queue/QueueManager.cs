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
                        .Include(q => q.Document.DocumentStatus)
                        .Include(q => q.Document.DocumentSubType.DocumentType)
                        .Include(q => q.Property)
                        .Include(q => q.Property.PropertyClass)
                        .Include(q => q.QueueStatus)
                        .Include(q => q.Department)
                        .Include(q => q.Employee)
                        //.Include(q => q.QueueNotes)
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
                db.Configuration.ProxyCreationEnabled = false;
                //db.Configuration.LazyLoadingEnabled = false;
                if (includeDetails)
                {
                    ; queues = db.Queues
                         .Include(q => q.Document)
                         .Include(q => q.Document.DocumentStatus)
                         .Include(q => q.Document.DocumentSubType.DocumentType)
                         .Include(q => q.Property)
                         .Include(q => q.QueueStatus)
                         .Include(q => q.Department)
                         .Include(q => q.Employee)
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
                //var currentQueue = db.Queues.Where(q => q.QueueID == queue.QueueID).First();

                db.Queues.Add(queue);
                db.SaveChanges();

            }

            return queue.QueueID;
        }

        public void AssignEmployeeBulk(List<int> queueIDs, int employeeID)
        {
            using (var db = new ModelDBContext())
            {
                var queueList = db.Queues.Where(q => queueIDs.Contains(q.QueueID)).ToList();

                foreach (Queue q in queueList)
                {
                    q.EmployeeID = employeeID;
                }

                db.SaveChanges();   

            }

           
        }

    }


}
