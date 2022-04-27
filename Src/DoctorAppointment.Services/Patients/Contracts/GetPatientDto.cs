using DoctorAppointment.Entities.Appointments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Patients.Contracts
{
    public class GetPatientDto 
    {
        public int Id { get; set; }
        public string NationalCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public GetPatientDto()
        {
            Appointments = new HashSet<Appointment> { };
        }

        public HashSet<Appointment> Appointments { get; set; }
    }
}
