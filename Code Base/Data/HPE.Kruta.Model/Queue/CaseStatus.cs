namespace HPE.Kruta.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CaseStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CaseStatus()
        {
            PropertyCases = new HashSet<PropertyCase>();
        }

        [Key]
        public int CaseStatusID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyCase> PropertyCases { get; set; }
    }
}
