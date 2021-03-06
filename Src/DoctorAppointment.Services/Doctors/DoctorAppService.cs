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

        public void Add(AddDoctorDto doctor)
        {
            var addedDoctor = new Doctor()
            {
                Field = doctor.Field,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                NationalCode = doctor.NationalCode,

            };
            _repository.Add(addedDoctor);
            _unitOfWork.Commit();   
        }

        public void Delete(int id)
        {
            var doctor = _repository.FindById(id);

            if(doctor == null)
            {
                throw new DoctorWithThisIdDoesNotExistException();
            }
            _repository.Delete(doctor);
            _unitOfWork.Commit();

        }

        public List<GetDoctorDto> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(int id, UpdateDoctorDto updatedDoctor)
        {
            var doctor = _repository.FindById(id);
            if (doctor == null)
            {
                throw new DoctorWithThisIdDoesNotExistException();
            }
            doctor.FirstName = updatedDoctor.FirstName;
            doctor.LastName = updatedDoctor.LastName;   
            doctor.NationalCode = updatedDoctor.NationalCode;   
            doctor.Field = updatedDoctor.Field;

            _repository.Update(doctor);
            _unitOfWork.Commit();

        }
    }
} 
