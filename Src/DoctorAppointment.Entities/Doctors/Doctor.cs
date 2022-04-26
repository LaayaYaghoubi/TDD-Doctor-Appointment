using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Entities.Persons;

namespace DoctorAppointment.Entities.Doctors
{
    public class Doctor : Person
    {

        public Doctor()
        {
            Appointments = new HashSet<Appointment> { };
        }
           
         
        public string Field { get; set; }
        public HashSet<Appointment> Appointments { get; set; }
        
    }
}
