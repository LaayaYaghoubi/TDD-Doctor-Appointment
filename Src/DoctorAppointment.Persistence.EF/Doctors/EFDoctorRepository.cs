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

        public void Delete(Doctor doctor)
        {
            _dataContext.Doctors.Remove(doctor);
        }

        public Doctor FindById(int id)
        {
            return _dataContext.Doctors.Find(id);
        }

        public List<GetDoctorDto> GetAll()
        {
            return _dataContext.Doctors.Select(_ => new GetDoctorDto
            {
                FirstName = _.FirstName,
                LastName = _.LastName,
                NationalCode = _.NationalCode,
                Field = _.Field,
            }).ToList();

        }

        public bool IsDoctorExist(int id)
        {
            return _dataContext.Doctors.Any(x => x.Id == id);   
        }

        public void Update(Doctor doctor)
        {
           // _dataContext.Doctors.Update(doctor);
        }
    }
}
