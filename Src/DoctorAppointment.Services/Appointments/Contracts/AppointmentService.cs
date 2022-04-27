using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts
{
    public interface AppointmentService : Service
    {
        void Add(Appointment appointment);
        void Update(int id, Appointment updatedAppointment);
        void Delete(int id);
        List<Appointment> GetAll();
    }
}
