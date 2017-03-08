namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable]
    public partial class QueueStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QueueStatus()
        {
            Queues = new HashSet<Queue>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueueStatusID { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queues { get; set; }
    }
}
