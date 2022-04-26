using DoctorAppointment.Entities.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Tests.Tools.Doctors
{
    public class DoctorBuilder
    {
        Doctor doctor = new Doctor();

        public DoctorBuilder()
        {
            doctor = new Doctor
            {
                FirstName = "Laaya",
                LastName = "Yaghoubi",
                NationalCode = "3245",
                Field = "Ghalb"
            };
        }

        public void WithFistName(string FistName)
        {
            doctor.FirstName = FistName;
        }

        public void WithLastName(string Lastname)
        {
            doctor.LastName = Lastname;
        }

       public void  WithNationalCode(string nationalcode)
        {
            doctor.NationalCode = nationalcode;
        }
        public void WithField(string field)
        {
            doctor.Field = field;
        }
        public Doctor CreateDoctor()
        {
            return doctor;
        }
    }
}
