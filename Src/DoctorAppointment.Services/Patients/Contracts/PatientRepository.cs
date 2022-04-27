using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public interface PatientRepository : Repository
    {
        void Add(AddPatientDto patient);
        Patient FindById(int id);
        void Update(Patient patient);
        void Delete(Patient patient);
        List<Patient> GetAll();
    }
}
