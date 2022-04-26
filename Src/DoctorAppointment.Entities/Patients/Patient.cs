using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Persons;

namespace DoctorAppointment.Entities.Patients
{
    public class Patient : Person
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment> {};
        }

        public HashSet<Appointment> Appointments { get; set; }   
    }
}
