using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMedCalendar.Models;

namespace MyMedCalendar.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // DbSets for the entities
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<MedicationSchedule> MedicationSchedules { get; set; } = null!;
        public DbSet<Drug> Drugs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ApplicationUser fields
            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.InsertedAt)
                .HasDefaultValueSql("GETUTCDATE()") // Default to current UTC time
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.ModifiedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            // Patient and MedicationSchedule relationship
            modelBuilder.Entity<MedicationSchedule>()
                .HasOne(ms => ms.Patient)
                .WithMany(p => p.MedicationSchedules)
                .HasForeignKey(ms => ms.PatientId);

            // Patient entity configuration
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.Id); // Primary Key
            modelBuilder.Entity<Patient>()
                .Property(p => p.Id)
                .ValueGeneratedNever();


            modelBuilder.Entity<Patient>()
                .HasIndex(p => p.UserId)
                .HasDatabaseName("IX_Patient_UserId"); // Index for fast lookup


            modelBuilder.Entity<Patient>()
                .Property(p => p.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(p => p.LastName)
                .HasMaxLength(100)
                .IsRequired();
            modelBuilder.Entity<Patient>()
                .Property(p => p.AMKA)
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Patient>()
              .Property(p => p.InsertedAt)
              .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<Patient>()
                .Property(p => p.ModifiedAt)
                .HasDefaultValueSql("GETUTCDATE()");


            modelBuilder.Entity<Drug>()
                .Property(d => d.Name)
                .HasMaxLength(200)
                .IsRequired();


            // Drug entity configuration
            modelBuilder.Entity<Drug>()
                .HasKey(d => d.Id); // Primary Key
            modelBuilder.Entity<Drug>()
                .HasIndex(d => d.Name)
                .HasDatabaseName("IX_Drug_Name"); // Index for fast search

            // MedicationSchedule entity configuration
            modelBuilder.Entity<MedicationSchedule>()
                .HasKey(ms => ms.Id); // Primary Key

           
            
            
        }
    }
}
