using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DoctorAppointment.RestAPI.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _service;

        public AppointmentController(AppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(Appointment appointment)
        {
            _service.Add(appointment);
        }

        [HttpGet]
        public List<Appointment> GetAll()
        {
            return _service.GetAll();
        }

        [HttpPut]
        public void Update(int id, Appointment appointment)
        {
            _service.Update(id, appointment);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}

