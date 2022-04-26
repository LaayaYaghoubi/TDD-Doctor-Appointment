using DoctorAppointment.Entities.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Tests.Tools.Patients
{
    public class PatientBuilder
    {
        Patient patient = new Patient();

        public PatientBuilder()
        {
            patient = new Patient()
            {
                FirstName = "lili",
                LastName = "potter",
                NationalCode = "33467",
            };
        }
            public PatientBuilder WithFirstName(string firstName)
            {
                patient.FirstName = firstName;
                return this;
            }
           public PatientBuilder WithLastName(string lastName)
            {
            patient.LastName = lastName;
            return this;
            }
            public PatientBuilder WithNationalCode(string nationalCode)
            {
            patient.NationalCode = nationalCode;
            return this;
            }
        public Patient CreatePatient()
        {
            return patient;
        }


        
    }
    }

