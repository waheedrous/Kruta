using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPE.Kruta.DataAccess.Entities
{


    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int EmpID { get; set; }

        [Required]
        [StringLength(255)]
        public string EmpNm { get; set; }

        [StringLength(64)]
        public string UserNm { get; set; }

        public int? DepartmentID { get; set; }
    }
}
