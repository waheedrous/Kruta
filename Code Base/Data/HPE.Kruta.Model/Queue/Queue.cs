namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Queue")]
    public partial class Queue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Queue()
        {
            QueueHistories = new HashSet<QueueHistory>();
        }

        [Key]
        public int QueueID { get; set; }

        public int? QueueStatusID { get; set; }

        public int? PropertyID { get; set; }

        public int? DepartmentID { get; set; }

        public int? EmployeeID { get; set; }

        public int? DocumentID { get; set; }

        public DateTime? ReceivedDateTime { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public virtual Department Department { get; set; }

        public virtual Document Document { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Property Property { get; set; }

        public virtual QueueStatus QueueStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QueueHistory> QueueHistories { get; set; }
    }
}
