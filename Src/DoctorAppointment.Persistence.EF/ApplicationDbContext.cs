using DoctorAppointment.Entities;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Entities.Patients;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(string connectionString) :
           this(new DbContextOptionsBuilder().UseSqlServer(connectionString).Options)
        { }

     
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(ApplicationDbContext).Assembly);
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
