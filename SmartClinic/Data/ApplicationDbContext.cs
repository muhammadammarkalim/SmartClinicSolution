using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartClinic.Models;

namespace SmartClinic.Data
{
    /// <summary>
    /// Database context handling table mapping, configuration constraints, and auto-seeding templates.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Database Tables
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specialty> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Always call the base Identity configuration first
            base.OnModelCreating(builder);

            // 1. Doctor -> AppUser (Restrict delete: deleting user shouldn't implicitly delete records cascade style)
            builder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Appointment -> Patient (AppUser) Restrict
            builder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Restrict);

            // 3. Appointment -> Doctor Restrict
            builder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            // 4. Prescription -> Appointment (One-to-One Unique relation)
            builder.Entity<Prescription>()
                .HasOne(p => p.Appointment)
                .WithOne(a => a.Prescription)
                .HasForeignKey<Prescription>(p => p.AppointmentID)
                .OnDelete(DeleteBehavior.Restrict);

            // 5. PrescriptionMedicine -> Prescription (Cascade Delete: if a prescription is removed, its medicines go too)
            builder.Entity<PrescriptionMedicine>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.Medicines)
                .HasForeignKey(pm => pm.PrescriptionID)
                .OnDelete(DeleteBehavior.Cascade);

            // 6. Review composite unique index (Prevents a single patient from leaving multiple reviews on the same doctor)
            builder.Entity<Review>()
                .HasIndex(r => new { r.DoctorID, r.PatientID })
                .IsUnique();
        }
    }
}