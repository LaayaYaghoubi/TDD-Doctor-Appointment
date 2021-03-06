using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Services.Test.Unit.Appointments;
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
        public void Add(AddAppointmentDto appointment)
        {
            _service.Add(appointment);
        }

        [HttpGet]
        public List<GetAllAppointmentDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpPut]
        public void Update(int id, UpdateAppointmentDto appointment)
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

