using System;
using System.Runtime.Serialization;

namespace DoctorAppointment.Services.Patients
{
    [Serializable]
    public class PatientWithThisIdDoesNotExistException : Exception
    {
       
    }
}