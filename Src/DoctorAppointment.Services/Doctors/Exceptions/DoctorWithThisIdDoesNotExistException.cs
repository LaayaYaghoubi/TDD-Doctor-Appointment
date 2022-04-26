using System;
using System.Runtime.Serialization;

namespace DoctorAppointment.Services.Doctors
{
    [Serializable]
    public class DoctorWithThisIdDoesNotExistException : Exception
    {
       
    }
}