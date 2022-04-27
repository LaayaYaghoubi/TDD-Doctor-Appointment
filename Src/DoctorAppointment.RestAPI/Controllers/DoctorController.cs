using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DoctorAppointment.RestAPI.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _service;

        public DoctorController(DoctorService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(Doctor doctor)
        {
            _service.Add(doctor);
        }

        [HttpGet]
        public List<Doctor> GetAll()
        {
           return _service.GetAll();
        }

        [HttpPut]
        public void Update(int id, Doctor doctor)
        {
            _service.Update(id, doctor);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
