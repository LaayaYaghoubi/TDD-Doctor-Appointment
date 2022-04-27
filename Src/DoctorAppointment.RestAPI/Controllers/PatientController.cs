using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DoctorAppointment.RestAPI.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _service;

        public PatientController(PatientService service)
        {
            _service = service;
        }

        [HttpPost]
        public void Add(AddPatientDto patient)
        {
            _service.Add(patient);
        }

        [HttpGet]
        public List<GetPatientDto> GetAll()
        {
            return _service.GetAll();
        }

        [HttpPut]
        public void Update(int id, UpdatePatientDto patient)
        {
            _service.Update(id, patient);
        }
        [HttpDelete]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}

