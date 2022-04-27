using System;
using System.Runtime.Serialization;

namespace DoctorAppointment.Services.Appointments
{
    [Serializable]
    public class DoctorAppointmentsAreFullException : Exception
    {
       
    }
}