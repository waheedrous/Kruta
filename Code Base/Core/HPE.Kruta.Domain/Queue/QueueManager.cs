using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace HPE.Kruta.Domain
{
    public class QueueManager
    {
        public const string ASSIGNED_QUEUE_STATUS = "Assigned";

        public Queue Get(int queueID, bool includeDetails)
        {
            Queue queue;
            using (var db = new ModelDBContext())
            {
                if (includeDetails)
                {
                    var query = db.Queues.Where(q => q.QueueID == queueID)
                        .Include(q => q.Document)
                        .Include(q => q.Document.DocumentStatus)
                        .Include(q => q.Document.DocumentSubType.DocumentType)
                        .Include(q => q.Property)
                        .Include(q => q.Property.PropertyClass)
                        .Include(q => q.QueueStatus)
                        .Include(q => q.Department)
                        .Include(q => q.Employee)
                        .Include(q => q.QueueNotes.Select(qn => qn.Employee));
                    //.First();
                    queue = query.First();

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
                    queues = db.Queues
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
                var assignedQueueStatus = db.QueueStatus.FirstOrDefault(q => string.Compare(q.Description, ASSIGNED_QUEUE_STATUS, StringComparison.OrdinalIgnoreCase) == 0);

                if (assignedQueueStatus != null)
                {
                    foreach (Queue q in queueList)
                    {
                        q.QueueStatusID = assignedQueueStatus.QueueStatusID;
                        q.EmployeeID = employeeID;
                    }

                    db.SaveChanges();
                }
            }
        }

        public void RouteQueue(int queueID, int departmentID)
        {
            using (var db = new ModelDBContext())
            {
                var currentQueue = db.Queues.Where(q => q.QueueID == queueID).First();

                var queueHistory = new QueueHistory();
                queueHistory.QueneID = queueID;
                queueHistory.RoutedFromDepartmentID = currentQueue.DepartmentID;
                queueHistory.RoutedToDepartmentID = departmentID;
                queueHistory.AssignedFromEmployeeID = currentQueue.EmployeeID;
                queueHistory.EventDatetime = DateTime.Now;

                db.QueueHistories.Add(queueHistory);

                currentQueue.DepartmentID = departmentID;

                db.SaveChanges();
            }
        }

        public int UpdateQueueDocumentStatus(int queueID, int documentStatusID)
        {
            using (var db = new ModelDBContext())
            {
                var currentQueue = db.Queues.Where(q => q.QueueID == queueID).First();

                currentQueue.Document.DocumentStatusID = documentStatusID;

                db.SaveChanges();

            }

            return queueID;
        }
    }
}
