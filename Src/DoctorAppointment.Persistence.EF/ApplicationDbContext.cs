using DoctorAppointment.Entities;
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
    }
}
