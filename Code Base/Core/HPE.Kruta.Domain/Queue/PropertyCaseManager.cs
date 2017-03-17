using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using HPE.Kruta.Common.Enums;

namespace HPE.Kruta.Domain
{
    public class PropertyCaseManager
    {
        /// <summary>
        /// Get all cases from database.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PropertyCase> List()
        {
            List<PropertyCase> cases;
            using (var db = new ModelDBContext())
            {
                db.Configuration.ProxyCreationEnabled = false;

                cases = db.PropertyCases
                    .Include(c => c.Department)
                    .Include(c => c.Employee)
                    .Include(c => c.CaseType)
                    .Include(c => c.CaseStatus)
                    .ToList();
               
            }

            return cases;
        }

        public int CreateCase(List<int> selectedQueueIds, int departmentID, int caseTypeID)
        {

            using (var db = new ModelDBContext())
            {
                //get list of queues to be update later on
                var queueList = db.Queues.Where(q => selectedQueueIds.Contains(q.QueueID)).Include(q => q.Property).ToList();
                //get case type name
                string caseType = db.CaseTypes.Where(c => c.CaseTypeID == caseTypeID).Select(c => c.Name).FirstOrDefault();

                //generate case name
                string caseName = caseType + "-" + queueList[0].Property.ParcelNumber + "-" + DateTime.Now.ToString("MM-dd-yyyy");

                //create case object
                PropertyCase propertyCase = new PropertyCase()
                {
                    CaseTypeID = caseTypeID,
                    DepartmentID = departmentID,
                    CreatedDate = DateTime.Now,
                    CaseName = caseName
                };

                //save changes to get the case id
                db.PropertyCases.Add(propertyCase);
                db.SaveChanges();

                //assign case number to all queues
                foreach (var queue in queueList)
                {
                    queue.PropertyCaseID = propertyCase.PropertyCaseID;
                }

                db.SaveChanges();

                return propertyCase.PropertyCaseID;
            }
        }

        /// <summary>
        /// assigns bulk for queue items to an employee
        /// </summary>
        /// <param name="queueIDs">list of queue ids</param>
        /// <param name="employeeID">employee to assign to</param>
        public void AssignEmployeeBulk(List<int> queueIDs, int employeeID)
        {
            using (var db = new ModelDBContext())
            {
                var newQueueStatus = db.CaseStatus.FirstOrDefault(q => q.CaseStatusID == (int)QueueStatusEnum.New);
                var queueList = db.PropertyCases.Where(q => queueIDs.Contains(q.PropertyCaseID)).ToList();

                foreach (PropertyCase q in queueList)
                {
                    //only change it to new if the current status is null
                    if (newQueueStatus != null && q.CaseStatusID == null)
                        q.CaseStatusID = newQueueStatus.CaseStatusID;

                    q.EmployeeID = employeeID;
                }

                db.SaveChanges();
            }
        }


        public void RouteQueueList(List<int> selectedQueueIds, int departmentID)
        {
            using (var db = new ModelDBContext())
            {
                foreach (int queueID in selectedQueueIds)
                {
                    var currentQueue = db.PropertyCases.Where(q => q.PropertyCaseID == queueID).First();

                    if (currentQueue.DepartmentID != departmentID)
                    {
                        //create history record shows the old and new departments
                        //var queueHistory = new CaseHistory();
                        //queueHistory.CaseID = queueID;
                        //queueHistory.RoutedFromDepartmentID = currentQueue.DepartmentID;
                        //queueHistory.RoutedToDepartmentID = departmentID;
                        //queueHistory.AssignedFromEmployeeID = currentQueue.EmployeeID;
                        //queueHistory.EventDatetime = DateTime.Now;

                        //db.QueueHistories.Add(queueHistory);

                        currentQueue.DepartmentID = departmentID;
                        currentQueue.EmployeeID = null;

                        var inProgressQueueStatus = db.CaseStatus.FirstOrDefault(q => q.CaseStatusID == (int)QueueStatusEnum.InProgress);
                        if (inProgressQueueStatus != null)
                            currentQueue.CaseStatusID = inProgressQueueStatus.CaseStatusID;

                        db.SaveChanges();
                    }
                }
            }
        }
    }
}
