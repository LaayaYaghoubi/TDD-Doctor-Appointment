using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Patients
{
    public class EFPatientRepository : PatientRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public EFPatientRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Patient patient)
        {
           _dataContext.Patients.Add(patient);  
        }

        public void Delete(Patient patient)
        {
           _dataContext.Patients.Remove(patient);
        }

        public Patient FindById(int id)
        {
            return _dataContext.Patients.Find(id);
        }

        public List<GetPatientDto> GetAll()
        {
            return _dataContext.Patients.Select(_ => new GetPatientDto
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode,

            }).ToList(); 
        }

        public void Update(Patient patient)
        {
            _dataContext.Patients.Update(patient);
        }
    }
}
