namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable]
    [Table("Queue")]
    public partial class Queue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Queue()
        {
            //QueueHistories = new HashSet<QueueHistory>();
            //QueueNotes = new HashSet<QueueNote>();
        }

        [Key]
        public int QueueID { get; set; }

        public int? QueueStatusID { get; set; }

        public int? PropertyID { get; set; }

        public int? DepartmentID { get; set; }

        public int? EmployeeID { get; set; }

        public int? DocumentID { get; set; }

        public DateTime? ReceivedDateTime { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        [ForeignKey("DocumentID")]
        public virtual Document Document { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("PropertyID")]
        public virtual Property Property { get; set; }

        [ForeignKey("QueueStatusID")]
        public virtual QueueStatus QueueStatus { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<QueueHistory> QueueHistories { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<QueueNote> QueueNotes { get; set; }
    }
}
