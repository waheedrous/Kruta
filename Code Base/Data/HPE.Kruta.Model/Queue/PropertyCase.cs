namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PropertyCase")]
    public partial class PropertyCase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PropertyCase()
        {
            Queues = new HashSet<Queue>();
        }

        public int PropertyCaseID { get; set; }

        [StringLength(255)]
        public string CaseName { get; set; }

        public int? DepartmentID { get; set; }

        public int? EmployeeID { get; set; }

        public int? CaseTypeID { get; set; }

        public int? CaseStatusID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual CaseStatus CaseStatus { get; set; }

        public virtual CaseType CaseType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Queue> Queues { get; set; }
    }
}
