using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Services.Doctors.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Doctors
{
    public class DoctorAppService : DoctorService
    {
        private readonly DoctorRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public DoctorAppService(
            DoctorRepository repository,
            UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(Doctor doctor)
        {
            _repository.Add(doctor);
        }

        public void Update(int id, Doctor updatedDoctor)
        {
            var doctor = _repository.FindById(id);
            if (doctor == null)
            {
                throw new DoctorWithThisIdDoesNotExistException();
            }
            _repository.Update(doctor);
            _unitOfWork.Commit();

        }
    }
} 
