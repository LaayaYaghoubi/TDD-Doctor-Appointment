using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Services.Test.Unit.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Appointments.Contracts
{
    public interface AppointmentService : Service
    {
        void Add(AddAppointmentDto appointment);
        void Update(int id, UpdateAppointmentDto updatedAppointment);
        void Delete(int id);
        List<GetAllAppointmentDto> GetAll();
    }
}
