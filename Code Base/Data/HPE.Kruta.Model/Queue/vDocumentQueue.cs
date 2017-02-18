namespace HPE.Kruta.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vDocumentQueue")]
    public partial class vDocumentQueue
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QueneID { get; set; }

        public DateTime? ReceivedDateTime { get; set; }

        public DateTime? RecordedDateTime { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DocumentID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(32)]
        public string DocumentNumber { get; set; }

        [StringLength(256)]
        public string DepartmentName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(255)]
        public string EmployeeName { get; set; }

        [StringLength(20)]
        public string DocumentStatusDescription { get; set; }

        [StringLength(4)]
        public string DocumentTypeCode { get; set; }

        [StringLength(32)]
        public string ParcelNumber { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        [StringLength(255)]
        public string DocumentTypeDescription { get; set; }
    }
}
