using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
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

        public int DoctorAppointmentCount(int doctorId, int day)
        {
           return _dataContext.Appointments.Count
                (_ => _.DoctorId == doctorId && _.Date.Day == day);

        }
    }
}
