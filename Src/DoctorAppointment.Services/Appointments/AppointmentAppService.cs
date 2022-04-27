using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Services.Appointments.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments
{
    public class AppointmentAppService : AppointmentService
    {
        private readonly AppointmentRepository _repository;
        private readonly UnitOfWork _unitOfWork;

        public AppointmentAppService(AppointmentRepository repository, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Add(Appointment appointment)
        {
            int DoctorAppointmentCount = _repository.
                DoctorAppointmentCount(appointment.DoctorId, appointment.Date.Day);

            if (DoctorAppointmentCount >= 5)
            {
                throw new DoctorAppointmentsAreFullException();
            }
            _repository.Add(appointment);
            _unitOfWork.Commit();   
        }

        public void Update(int id, Appointment updatedAppointment)
        {
            var appointment = _repository.IsAppointmentExist(id);

            if (appointment == null)
            {
                throw new ThereIsNoAppointmentWithThisIdException();
            }
            appointment.PatientId = updatedAppointment.PatientId;
            appointment.Date = updatedAppointment.Date; 
            appointment.DoctorId = updatedAppointment.DoctorId; 

            _repository.Update(appointment);
            _unitOfWork.Commit();
        }
    }
}
