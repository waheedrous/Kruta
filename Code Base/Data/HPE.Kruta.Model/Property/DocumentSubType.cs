namespace HPE.Kruta.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable]
    [Table("DocumentSubType")]
    public partial class DocumentSubType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentSubType()
        {
            //Documents = new HashSet<Document>();
        }

        [Key]
        public int DocumentSubTypeID { get; set; }

        [StringLength(4)]
        public string Code { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public int? DocumentTypeID { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Document> Documents { get; set; }

        [ForeignKey("DocumentTypeID")]
        public virtual DocumentType DocumentType { get; set; }
    }
}
