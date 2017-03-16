using System;

namespace HPE.Kruta.Model.ViewModels
{
    public class QueueWithSequence
    {
        public QueueWithSequence()
        {
          
        }

        public int QueueID { get; set; }

        public int Sequence { get; set; }

        //public DateTime? RecordedDateTime { get; set; }

        private DateTime? _recordedDateTime;

        public DateTime? RecordedDateTime
        {
            get { return _recordedDateTime?.Date; }
            set { _recordedDateTime = value; }
        }


        public int? EmployeeID { get; set; }

        public string EmployeeName { get; set; }

        //public DateTime? ReceivedDateTime { get; set; }

        private DateTime? _receivedDateTime;

        public DateTime? ReceivedDateTime
        {
            get { return _receivedDateTime?.Date; }
            set { _receivedDateTime = value; }
        }

        public int? DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentCode { get; set; }

        public int? DocumentID { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentType { get; set; }

        public int? DocumentStatusID { get; set; }

        public string DocumentStatus { get; set; }

        public int? PropertyID { get; set; }

        public string ParcelNumber { get; set; }

        public int? QueueStatusID { get; set; }

        public string QueueStatus { get; set; }

        public string DocumentSubTypeCode { get; set; }

        public int? PropertyCaseID { get; set; }
    }
}

