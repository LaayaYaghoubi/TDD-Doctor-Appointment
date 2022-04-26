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

        public DoctorBuilder WithFistName(string FistName)
        {
            doctor.FirstName = FistName;
            return this;
        }

        public DoctorBuilder WithLastName(string Lastname)
        {
            doctor.LastName = Lastname;
            return this;
        }

       public DoctorBuilder WithNationalCode(string nationalcode)
        {
            doctor.NationalCode = nationalcode;
            return this; 
        }
        public DoctorBuilder WithField(string field)
        {
            doctor.Field = field;
            return this;
        }
        public Doctor CreateDoctor()
        {
            return doctor;
        }
    }
}
