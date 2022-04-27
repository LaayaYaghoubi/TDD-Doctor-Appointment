using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts
{
    public interface AppointmentRepository : Repository
    {
        void Add(Appointment appointment);
        int DoctorAppointmentCount(int doctorId, int day);
    }
}
