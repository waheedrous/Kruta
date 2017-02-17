namespace HPE.Kruta.DataAccess
{
    using Entities;
    using System.Data.Entity;

    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=KRUTAConnection")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.EmpNm)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UserNm)
                .IsUnicode(false);
        }
    }
}
