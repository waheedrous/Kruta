namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QueueHistory")]
    public partial class QueueHistory
    {
        public int QueueHistoryID { get; set; }

        public int? QueueID { get; set; }

        public int? RoutedToDepartmentID { get; set; }

        public int? RoutedFromDepartmentID { get; set; }

        public int? AssignedFromEmployeeID { get; set; }

        public DateTime? EventDatetime { get; set; }

        public virtual Department Department { get; set; }

        public virtual Department Department1 { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Queue Queue { get; set; }
    }
}
