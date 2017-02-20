namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Document")]
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            Queues = new HashSet<Queue>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentID { get; set; }

        [Required]
        [StringLength(32)]
        public string DocumentNumber { get; set; }

        public int? DocumentSubTypeID { get; set; }

        public DateTime? RecordedDateTime { get; set; }

        public int? DocumentStatusID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queues { get; set; }

        public virtual DocumentStatus DocumentStatu { get; set; }

        public virtual DocumentSubType DocumentSubType { get; set; }
    }
}
