using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Doctors
{
    public class EFDoctorRepository : DoctorRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public EFDoctorRepository(ApplicationDbContext dbContext)
        {
            _dataContext = dbContext;
        }

        public void Add(Doctor doctor)
        {
            _dataContext.Doctors.Add(doctor);
        }
    }
}
