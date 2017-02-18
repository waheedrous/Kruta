    namespace HPE.Kruta.DataAccess
{
    using Model;
    using System.Data.Entity;

    public partial class ModelDBContext : DbContext
    {
        public ModelDBContext()
            : base("name=KRUTAConnection")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentStatu> DocumentStatus { get; set; }
        public virtual DbSet<DocumentSubType> DocumentSubTypes { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyClass> PropertyClasses { get; set; }
        public virtual DbSet<Queue> Queues { get; set; }
        public virtual DbSet<QueueStatu> QueueStatus { get; set; }
        public virtual DbSet<QueueHistory> QueueHistories { get; set; }

        public virtual DbSet<vDocumentQueue> vDocumentQueues { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Department>()
               .Property(e => e.DepartmentCode)
               .IsFixedLength()
               .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.QueueHistories)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.RoutedToDepartmentID);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.QueueHistories1)
                .WithOptional(e => e.Department1)
                .HasForeignKey(e => e.RoutedFromDepartmentID);

            modelBuilder.Entity<Document>()
                .Property(e => e.DocumentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentStatu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentSubType>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DocumentSubType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DocumentType>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.QueueHistories)
                .WithOptional(e => e.Employee)
                .HasForeignKey(e => e.AssignedFromEmployeeID);

            modelBuilder.Entity<Property>()
                .Property(e => e.ParcelNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Property>()
                .Property(e => e.FullLegalDescription)
                .IsUnicode(false);

            modelBuilder.Entity<PropertyClass>()
                .Property(e => e.Code)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PropertyClass>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<PropertyClass>()
                .HasMany(e => e.Properties)
                .WithOptional(e => e.PropertyClass)
                .HasForeignKey(e => e.PropertyClassID);

            modelBuilder.Entity<Queue>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<QueueStatu>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<QueueStatu>()
                .HasMany(e => e.Queues)
                .WithOptional(e => e.QueueStatu)
                .HasForeignKey(e => e.QueueStatus);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.DocumentNumber)
                .IsUnicode(false);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.EmployeeName)
                .IsUnicode(false);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.DocumentStatusDescription)
                .IsUnicode(false);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.DocumentTypeCode)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.ParcelNumber)
                .IsUnicode(false);

            modelBuilder.Entity<vDocumentQueue>()
                .Property(e => e.DocumentTypeDescription)
                .IsUnicode(false);
        }
    }
}
