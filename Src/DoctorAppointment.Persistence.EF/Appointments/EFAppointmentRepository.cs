using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Test.Unit.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF.Appointments
{
    public class EFAppointmentRepository : AppointmentRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public EFAppointmentRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Add(Appointment appointment)
        {
            _dataContext.Appointments.Add(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _dataContext.Appointments.Remove(appointment);
        }

        public int DoctorAppointmentCount(int doctorId, int day)
        {
           return _dataContext.Appointments.Count
                (_ => _.DoctorId == doctorId && _.Date.Day == day);

        }

        public Appointment FindById(int id)
        {
            return _dataContext.Appointments.FirstOrDefault(_ => _.Id == id);
        }

        public List<GetAllAppointmentDto> GetAll()
        {
            return _dataContext.Appointments.Select(_ => new GetAllAppointmentDto
            {
                DoctorId = _.DoctorId,
                PatientId = _.PatientId,
                Date = _.Date,
            }).ToList();
        }

        public Appointment IsAppointmentExist(int id)
        {
            return _dataContext.Appointments.FirstOrDefault(_ => _.Id == id);
        }

        public void Update(Appointment appointment)
        {
            _dataContext.Update(appointment);
        }
    }
}
