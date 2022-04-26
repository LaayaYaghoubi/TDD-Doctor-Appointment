using System;
using System.Runtime.Serialization;

namespace DoctorAppointment.Services.Doctors
{
    [Serializable]
    internal class DoctorWithThisIdDoesNotExistException : Exception
    {
       
    }
}