using System;

namespace DoctorAppointment.Services.Test.Unit.Appointments
{
    public class GetAllAppointmentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}