namespace HPE.Kruta.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Case")]
    public partial class Case
    {
        public int CaseID { get; set; }

        [StringLength(255)]
        public string CaseName { get; set; }

        public int? DepartmentID { get; set; }

        public int? AssignedTo { get; set; }

        public int? CaseTypeID { get; set; }

        [StringLength(50)]
        public string CaseStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual CaseType CaseType { get; set; }

        public virtual Department Department { get; set; }

        public virtual Employee AssignedToEmployee { get; set; }
    }
}
