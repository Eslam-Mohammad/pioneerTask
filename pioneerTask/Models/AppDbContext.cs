using Microsoft.EntityFrameworkCore;

namespace pioneerTask.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePropertyDefinition> EmployeePropertyDefinitions { get; set; }
        public DbSet<DropdownOption> DropdownOptions { get; set; }
        public DbSet<EmployeePropertyValue> EmployeePropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.PropertyValues)
                .WithOne(pv => pv.Employee)
                .HasForeignKey(pv => pv.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeePropertyDefinition>()
         .HasMany(pd => pd.DropdownOptions)
         .WithOne(o => o.EmployeePropertyDefinition)
         .HasForeignKey(o => o.EmployeePropertyDefinitionId)
         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EmployeePropertyDefinition>()
       .HasIndex(pd => pd.Name)
       .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Code)
                .IsUnique();

         
        }
    }
}
