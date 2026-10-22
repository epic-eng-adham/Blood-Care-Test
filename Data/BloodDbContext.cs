using BloodCareF.Controllers;
using BloodCareF.Models;
using Microsoft.EntityFrameworkCore;
namespace BloodCareF.Data
{
    public class BloodDbContext:DbContext
    {
        public DbSet<BloodRequest> BloodRequests {  get; set; }
        public DbSet<BloodType> bloodTypes { get; set; }
        public DbSet<Donor> donors { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Donation> donations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodType>()
                .HasIndex(b => b.TypeName)
                .IsUnique();
            modelBuilder.Entity<Donor>()
                .HasOne(d => d.BloodType)
                .WithMany()
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.BloodType)
                .WithMany()
                .HasForeignKey(p => p.BloodTypeID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany()
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Patient)
                .WithMany()
                .HasForeignKey(d => d.PAtientId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BloodRequest>()
                .HasOne(r => r.Patient)
                .WithMany()
                .HasForeignKey(r => r.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BloodRequest>()
                .HasOne(r => r.BloodType)
                .WithMany()
                .HasForeignKey(r => r.BloodTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BloodCareF;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
