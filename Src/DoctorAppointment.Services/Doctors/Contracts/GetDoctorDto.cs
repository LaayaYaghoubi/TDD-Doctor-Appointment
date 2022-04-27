using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Doctors.Contracts
{
    public class GetDoctorDto : Person
    {
        public GetDoctorDto()
        {
            Appointments = new HashSet<Appointment> { };
        }


        public string Field { get; set; }
        public HashSet<Appointment> Appointments { get; set; }
    }
}
