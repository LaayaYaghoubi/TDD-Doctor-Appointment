using DoctorAppointment.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Tests.Tools.Appointments
{
    public class AppintmentBuilder
    {
        Appointment appointment = new Appointment();
        public AppintmentBuilder()
        {
            appointment = new Appointment()
            {
                PatientId = 1,
                DoctorId = 1,
                Date = DateTime.Now,
            };
        }
        public AppintmentBuilder WithPatientId(int id)
        {
            appointment.PatientId = id;
            return this;
        }
        public AppintmentBuilder WithDoctorId(int id)
        {
            appointment.DoctorId = id;
            return this;
        }
        public AppintmentBuilder WithDate(DateTime date)
        {
            appointment.Date = date;
            return this;
        }

    }
    }
