using BookStore.Infrastructure.Test;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Patients;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Appointments;
using DoctorAppointment.Services.Appointments;
using DoctorAppointment.Services.Appointments.Contracts;
using DoctorAppointment.Tests.Tools.Appointments;
using DoctorAppointment.Tests.Tools.Doctors;
using DoctorAppointment.Tests.Tools.Patients;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointment.Services.Test.Unit.Appointments
{
    public class AppointmentServiceTests
    {
        private readonly AppointmentRepository _repository;
        private readonly AppointmentService _sut;
        private readonly UnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dataContext;

        public AppointmentServiceTests()
        {
            _dataContext =
                new EFInMemoryDatabase()
                .CreateDataContext<ApplicationDbContext>();
            _repository = new EFAppointmentRepository(_dataContext);
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _sut = new AppointmentAppService(_repository,_unitOfWork);
        }



        [Fact]
        public void Add_adds_appintment_properly()
        {
            var doctor = new DoctorBuilder().CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patient = new PatientBuilder().CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));  
            var appointment = new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .WithDate(DateTime.UtcNow)
                .SetAppointment();

            _sut.Add(appointment);

            var expected = _dataContext.Appointments.FirstOrDefault();
            expected.DoctorId.Should().Be(appointment.DoctorId);
            expected.PatientId.Should().Be(appointment.PatientId);
            expected.Date.Should().Be(appointment.Date);
        }
        [Fact]
        public void Add_throws_Exception_DoctorAppointmentsAreFullException_if_doctor_appointments_become_more_than_5_in_one_day()
        {
            var doctor = new DoctorBuilder().CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            List<Patient> patients = CreateSixPatient();
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
            List<Appointment> appointments = SetAppointmetForSixPatient(doctor, patients);

            _sut.Add(appointments[0]);
            _sut.Add(appointments[1]);
            _sut.Add(appointments[2]);
            _sut.Add(appointments[3]);
            _sut.Add(appointments[4]);
            Action expected = () => _sut.Add(appointments[5]);

            expected.Should().ThrowExactly<DoctorAppointmentsAreFullException>();
        }

        private static List<Appointment> SetAppointmetForSixPatient(Entities.Doctors.Doctor doctor, List<Patient> patients)
        {
            return new List<Appointment>()
            {
                new AppintmentBuilder().WithDoctorId(doctor.Id)
                .WithPatientId(patients[0].Id)
                .SetAppointment(),
                new AppintmentBuilder().
                WithDoctorId(doctor.Id)
                .WithPatientId(patients[1].Id)
                .SetAppointment(),
                new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patients[2].Id)
                .SetAppointment(),
                new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patients[3].Id)
                .SetAppointment(),
                new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patients[4].Id)
                .SetAppointment(),
                 new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patients[5].Id)
                .SetAppointment(),
            };
        }

        private static List<Patient> CreateSixPatient()
        {
            return new List<Patient>()
            {
                new PatientBuilder().CreatePatient(),
                new PatientBuilder().WithNationalCode("23789").CreatePatient(),
                new PatientBuilder().WithFirstName("delvin").CreatePatient(),
                new PatientBuilder().WithLastName("asadi").CreatePatient(),
                new PatientBuilder().WithFirstName("laaya").CreatePatient(),
                new PatientBuilder().WithNationalCode("1001").CreatePatient(),

            };
        }
    }
}
