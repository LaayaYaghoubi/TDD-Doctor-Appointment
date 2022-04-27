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
    public interface AppointmentRepository : Repository
    {
        void Add(Appointment appointment);
        int DoctorAppointmentCount(int doctorId, int day);
        Appointment IsAppointmentExist(int id);
        void Update(Appointment appointment);
        Appointment FindById(int id);
        void Delete(Appointment appointment);
        List<GetAllAppointmentDto> GetAll();
    }
}
