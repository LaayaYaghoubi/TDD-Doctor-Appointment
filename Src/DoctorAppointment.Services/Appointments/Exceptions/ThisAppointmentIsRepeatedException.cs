using System;
using System.Runtime.Serialization;

namespace DoctorAppointment.Services.Appointments
{
    [Serializable]
    internal class ThisAppointmentIsRepeatedException : Exception
    {
        public ThisAppointmentIsRepeatedException()
        {
        }

        public ThisAppointmentIsRepeatedException(string message) : base(message)
        {
        }

        public ThisAppointmentIsRepeatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ThisAppointmentIsRepeatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}