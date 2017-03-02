using HPE.Kruta.Common.Enum;
using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using HPE.Kruta.Model.ViewModels;
using System;
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

        public List<QueueWithSequence> ListWithSequence()
        {

            List<QueueWithSequence> queues;
            using (var db = new ModelDBContext())
            {
                var queueList = db.Queues
                        .Include(q => q.Document)
                        .Include(q => q.Document.DocumentStatus)
                        .Include(q => q.Document.DocumentSubType.DocumentType)
                        .Include(q => q.Property)
                        .Include(q => q.QueueStatus)
                        .Include(q => q.Department)
                        .Include(q => q.Employee)
                        .ToList();

                queues = AddSequenceToList(queueList);

            }

            return queues;
        }

        public List<QueueWithSequence> AddSequenceToList(List<Queue> queueList)
        {
            return queueList.Select((q, seq) =>
                                    new QueueWithSequence
                                    {
                                        QueueID = q.QueueID,
                                        Sequence = seq + 1,
                                        DepartmentID = q.DepartmentID,
                                        DepartmentName = q.Department?.DepartmentName,
                                        DocumentStatus = q.Document?.DocumentStatus?.Description,
                                        DocumentID = q.DocumentID,
                                        DocumentNumber = q.Document?.DocumentNumber,
                                        DocumentStatusID = q.Document?.DocumentStatusID,
                                        DocumentType = q.Document?.DocumentSubType?.DocumentType?.Description,
                                        EmployeeID = q.EmployeeID,
                                        EmployeeName = q.Employee?.EmployeeName,
                                        ParcelNumber = q.Property?.ParcelNumber,
                                        PropertyID = q.PropertyID,
                                        QueueStatus = q.QueueStatus?.Description,
                                        QueueStatusID = q.QueueStatusID,
                                        ReceivedDateTime = q.ReceivedDateTime,
                                        RecordedDateTime = q.Document?.RecordedDateTime

                                    }).ToList();
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
                var newQueueStatus = db.QueueStatus.FirstOrDefault(q => q.QueueStatusID == (int)QueueStatusEnum.New);
                var queueList = db.Queues.Where(q => queueIDs.Contains(q.QueueID)).ToList();

                foreach (Queue q in queueList)
                {
                    //only chnage it to new if the curent status is null
                    if (newQueueStatus != null && q.QueueStatusID == null)
                        q.QueueStatusID = newQueueStatus.QueueStatusID;

                    q.EmployeeID = employeeID;
                }

                db.SaveChanges();
            }
        }

        public void RouteQueue(int queueID, int departmentID)
        {
            using (var db = new ModelDBContext())
            {
                var currentQueue = db.Queues.Where(q => q.QueueID == queueID).First();

                var queueHistory = new QueueHistory();
                queueHistory.QueueID = queueID;
                queueHistory.RoutedFromDepartmentID = currentQueue.DepartmentID;
                queueHistory.RoutedToDepartmentID = departmentID;
                queueHistory.AssignedFromEmployeeID = currentQueue.EmployeeID;
                queueHistory.EventDatetime = DateTime.Now;

                db.QueueHistories.Add(queueHistory);

                currentQueue.DepartmentID = departmentID;
                currentQueue.EmployeeID = null;

                var inProgressQueueStatus = db.QueueStatus.FirstOrDefault(q => q.QueueStatusID == (int)QueueStatusEnum.InProgress);
                if (inProgressQueueStatus != null)
                    currentQueue.QueueStatusID = inProgressQueueStatus.QueueStatusID;

                db.SaveChanges();
            }
        }

        public int UpdateQueueDocumentStatus(int queueID, int documentStatusID)
        {
            using (var db = new ModelDBContext())
            {
                var currentQueue = db.Queues.Where(q => q.QueueID == queueID).First();

                currentQueue.Document.DocumentStatusID = documentStatusID;

                var inProgressQueueStatus = db.QueueStatus.FirstOrDefault(q => q.QueueStatusID == (int)QueueStatusEnum.InProgress);
                if (inProgressQueueStatus != null)
                    currentQueue.QueueStatusID = inProgressQueueStatus.QueueStatusID;

                db.SaveChanges();

            }

            return queueID;
        }
    }
}
