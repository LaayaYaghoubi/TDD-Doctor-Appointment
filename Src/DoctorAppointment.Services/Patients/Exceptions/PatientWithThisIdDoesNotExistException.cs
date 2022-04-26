using System;
using System.Runtime.Serialization;

namespace DoctorAppointment.Services.Patients
{
    [Serializable]
    internal class PatientWithThisIdDoesNotExistException : Exception
    {
        public PatientWithThisIdDoesNotExistException()
        {
        }

        public PatientWithThisIdDoesNotExistException(string message) : base(message)
        {
        }

        public PatientWithThisIdDoesNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected PatientWithThisIdDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}