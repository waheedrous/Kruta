using Microsoft.VisualStudio.TestTools.UnitTesting;
using HPE.Kruta.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPE.Kruta.Model;
using HPE.Kruta.Model.ViewModels;

namespace HPE.Kruta.Domain.Tests
{
    [TestClass()]
    public class QueueManagerTest
    {
        [TestMethod()]
        public void AddSequenceToListTest()
        {

            List<Queue> qList = new List<Queue>();
            qList.Add(new Queue { QueueID = 1, DepartmentID = 1, EmployeeID = 1, PropertyID = 1 });
            qList.Add(new Queue { QueueID = 2, DepartmentID = 2, EmployeeID = 2, PropertyID = 2 });
            qList.Add(new Queue { QueueID = 3, DepartmentID = 3, EmployeeID = 3, PropertyID = 3 });


            QueueManager queueManager = new QueueManager();
            List<QueueWithSequence> qWithSequence = queueManager.AddSequenceToList(qList);

            Assert.AreEqual(1, qWithSequence[0].Sequence);
            Assert.AreEqual(2, qWithSequence[1].Sequence);
            Assert.AreEqual(3, qWithSequence[2].Sequence);
            
        }
    }
}