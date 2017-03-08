namespace HPE.Kruta.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable]
    [Table("Property")]
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            //Queues = new HashSet<Queue>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int PropertyID { get; set; }

        [StringLength(32)]
        public string ParcelNumber { get; set; }

        [StringLength(255)]
        public string FullLegalDescription { get; set; }

        public int? PropertyClassID { get; set; }

        public int? DepartmentID { get; set; }

        //[ForeignKey("DepartmentID")]
        //public virtual Department Department { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<Queue> Queues { get; set; }

        [ForeignKey("PropertyClassID")]
        public virtual PropertyClass PropertyClass { get; set; }
    }
}
