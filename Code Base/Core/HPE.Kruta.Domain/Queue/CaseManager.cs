using HPE.Kruta.DataAccess;
using HPE.Kruta.Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using HPE.Kruta.Common.Enum;

namespace HPE.Kruta.Domain
{
    public class CaseManager
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
