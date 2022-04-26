using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Patients
{
    public class PatientAppService : PatientService
    {
        private readonly PatientRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public PatientAppService(
            PatientRepository repository,
            UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(Patient patient)
        {
            _repository.Add(patient);
        }

        public void Update(int id, Patient updatedPatient)
        {
            var patient = _repository.FindById(id);
            if (patient == null)
            {
                throw new PatientWithThisIdDoesNotExistException();
            }
            _repository.Update(patient);
        }
    }
}
