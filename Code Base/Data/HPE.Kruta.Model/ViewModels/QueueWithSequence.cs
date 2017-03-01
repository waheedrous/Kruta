using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPE.Kruta.Model.ViewModels
{
    public class QueueWithSequence
    {
        public QueueWithSequence()
        {
          
        }

        public int QueueID { get; set; }

        public int Sequence { get; set; }

        public DateTime? RecordedDateTime { get; set; }

        public int? EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        public DateTime? ReceivedDateTime { get; set; }

        public int? DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public int? DocumentID { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int? DocumentStatusID { get; set; }

        public string DocumentStatus { get; set; }

        public int? PropertyID { get; set; }

        public string ParcelNumber { get; set; }

        public int? QueueStatusID { get; set; }

        public string QueueStatus { get; set; }

    }
}

