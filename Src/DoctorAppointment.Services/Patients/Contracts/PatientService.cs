
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public interface PatientService : Service
    {
        void Add(Patient patient);
        void Update(int id, Patient updatedPatient);
        void Delete(int id);
        List<Patient> GetAll();
    }
}
