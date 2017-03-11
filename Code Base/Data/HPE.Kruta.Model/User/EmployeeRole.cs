namespace HPE.Kruta.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EmployeeRole")]
    public partial class EmployeeRole
    {
        public int EmployeeRoleID { get; set; }

        public int EmployeeID { get; set; }

        public int RoleID { get; set; }

        public virtual Role Role { get; set; }
    }
}
