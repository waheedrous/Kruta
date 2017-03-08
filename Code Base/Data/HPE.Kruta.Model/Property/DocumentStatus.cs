namespace HPE.Kruta.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable]
    public partial class DocumentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DocumentStatus()
        {
            //Documents = new HashSet<Document>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentStatusID { get; set; }

        [StringLength(20)]
        public string Description { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Document> Documents { get; set; }
    }
}
