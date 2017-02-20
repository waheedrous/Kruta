namespace HPE.Kruta.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DocumentType")]
    public partial class DocumentType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentType()
        {
            DocumentSubTypes = new HashSet<DocumentSubType>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentTypeID { get; set; }

        [StringLength(4)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentSubType> DocumentSubTypes { get; set; }
    }
}
