using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Doctors.Contracts
{
    public interface DoctorRepository : Repository
    {
        void Add(Doctor doctor);
        Doctor FindById(int id);
        void Update(Doctor doctor);
        void Delete(Doctor doctor);
        List<Doctor> GetAll();
    }
}
