using BookStore.Infrastructure.Test;
using DoctorAppointment.Entities.Appointments;
using DoctorAppointment.Entities.Doctors;
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
            _sut = new AppointmentAppService(_repository, _unitOfWork);
        }



        [Fact]
        public void Add_adds_appintment_properly()
        {
            Doctor doctor = CreateADoctor();
            Patient patient = CreateAPatient();
            Appointment appointment = SetAnAppointment(doctor, patient);

            _sut.Add(appointment);

            var expected = _dataContext.Appointments.FirstOrDefault();
            expected.DoctorId.Should().Be(appointment.DoctorId);
            expected.PatientId.Should().Be(appointment.PatientId);
            expected.Date.Should().Be(appointment.Date);
        }



        [Fact]
        public void Add_throws_Exception_DoctorAppointmentsAreFullException_if_doctor_appointments_become_more_than_5_in_one_day()
        {
            Doctor doctor = CreateADoctor();
            List<Patient> patients = CreateSixPatients();
            List<Appointment> appointments = SetAppointmetForSixPatient(doctor, patients);

            _sut.Add(appointments[0]);
            _sut.Add(appointments[1]);
            _sut.Add(appointments[2]);
            _sut.Add(appointments[3]);
            _sut.Add(appointments[4]);
            Action expected = () => _sut.Add(appointments[5]);

            expected.Should().ThrowExactly<DoctorAppointmentsAreFullException>();
        }



        [Fact]
        public void Update_updates_an_appointment_properly()
        {
            Appointment appointment = SetAnAppointment();
            Appointment updatedAppointment = UpdateCreatedAppointment();
            

            _sut.Update(appointment.Id, updatedAppointment);

            var expected = _dataContext.Appointments.FirstOrDefault(_ => _.Id == updatedAppointment.Id);
            expected.PatientId.Should().Be(appointment.PatientId);
            expected.DoctorId.Should().Be(appointment.DoctorId);
            expected.Date.Should().Be(appointment.Date);
        }
        [Fact]
        public void Update_throws_exception_ThereIsNoAppointmentWithThisIdException_when_appointmemt_does_not_exist()
        {
            int fakeId = 576;
            Appointment appointment = SetAnAppointment();
            Appointment updatedAppointment = UpdateCreatedAppointment();
            _dataContext.Appointments.Add(updatedAppointment);

            Action expected = () => _sut.Update(fakeId, updatedAppointment);

            expected.Should().ThrowExactly<ThereIsNoAppointmentWithThisIdException>();
        }
        [Fact]
        public void Update_throws_exception_DoctorAppointmentsAreFullException_when_appointmemt_does_not_exist()
        {
            Doctor doctor = CreateADoctor();
            List<Patient> patients = CreateFivePatients();
            List<Appointment> appointments = SetTheirAppointments(doctor, patients);
            Patient newPatient = CreateANewPatient();
            Appointment newAppointment = SetANewAppointmentForNewPatient(doctor, newPatient);
            Appointment editedAppointment = EditNewPatientAppointmentDateToFivePatientAppointmentDate(doctor, appointments, newPatient);

            Action expected = () => _sut.Update(newAppointment.Id, editedAppointment);

            expected.Should().ThrowExactly<DoctorAppointmentsAreFullException>();
        }
        [Fact]
        public void Delete_deletes_appointment_properly()
        {
            Doctor doctor = CreateADoctor();
            Patient patient = CreateAPatient();
            Appointment appointment = SetAnAppointment(doctor, patient);
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));

            _sut.Delete(appointment.Id);

            _dataContext.Appointments.Should().NotContain(appointment);
        }
        [Fact]
        public void Delete_throws_ThereIsNoAppointmentWithThisIdException_when_appointment_does_not_exist_to_delete()
        {
            int fakeId = 590;
            Doctor doctor = CreateADoctor();
            Patient patient = CreateAPatient();
            Appointment appointment = SetAnAppointment(doctor, patient);
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));

            Action expected = () => _sut.Delete(fakeId);

            expected.Should().ThrowExactly<ThereIsNoAppointmentWithThisIdException>();

        }
        [Fact]
        public void GetAll_returns_all_appointments_properly()
        {
            Doctor doctor = CreateADoctor();
            List<Patient> patients = CreateFivePatients();
            List<Appointment> appointments = SetTheirAppointments(doctor, patients);

            var expected = _sut.GetAll();

            expected.Should().HaveCount(5);
            expected.Should().Contain(_ => _.DoctorId == appointments[0].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[0].PatientId);
            expected.Should().Contain(_ => _.Date == appointments[0].Date);
            expected.Should().Contain(_ => _.DoctorId == appointments[1].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[1].PatientId);
            expected.Should().Contain(_ => _.Date == appointments[1].Date);
            expected.Should().Contain(_ => _.DoctorId == appointments[2].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[2].PatientId);
            expected.Should().Contain(_ => _.Date == appointments[2].Date);
            expected.Should().Contain(_ => _.DoctorId == appointments[3].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[3].PatientId);
            expected.Should().Contain(_ => _.Date == appointments[3].Date);
            expected.Should().Contain(_ => _.DoctorId == appointments[4].DoctorId);
            expected.Should().Contain(_ => _.PatientId == appointments[4].PatientId);
            expected.Should().Contain(_ => _.Date == appointments[4].Date);
        }

        private Appointment EditNewPatientAppointmentDateToFivePatientAppointmentDate(Doctor doctor, List<Appointment> appointments, Patient newPatient)
        {
            var editedAppointment = new Appointment
            {
                DoctorId = doctor.Id,
                PatientId = newPatient.Id,
                Date = appointments[0].Date,
            };
            _dataContext.Manipulate(_ => _.Appointments.Add(editedAppointment));
            return editedAppointment;
        }

        private Appointment SetANewAppointmentForNewPatient(Doctor doctor, Patient newPatient)
        {
            var newAppointment = new AppintmentBuilder()
                .WithPatientId(newPatient.Id)
                .WithDoctorId(doctor.Id)
                .WithDate(DateTime.Parse("2022-04-27T05:21:13.390Z")).SetAppointment();
            _dataContext.Manipulate(_ => _.Appointments.Add(newAppointment));
            return newAppointment;
        }

        private Patient CreateANewPatient()
        {
            var newPatient = new PatientBuilder().WithNationalCode("654321").CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(newPatient));
            return newPatient;
        }

        private List<Appointment> SetTheirAppointments(Doctor doctor, List<Patient> patients)
        {
            var appointments = new List<Appointment>()
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
            };
            _dataContext.Manipulate(_ => _.Appointments.AddRange(appointments));
            return appointments;
        }

        private List<Patient> CreateFivePatients()
        {
            var patients = new List<Patient> {
                new PatientBuilder().CreatePatient(),
                new PatientBuilder().WithNationalCode("23789").CreatePatient(),
                new PatientBuilder().WithFirstName("delvin").CreatePatient(),
                new PatientBuilder().WithLastName("asadi").CreatePatient(),
                new PatientBuilder().WithFirstName("laaya").CreatePatient(),
            };
            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
            return patients;
        }

        private static Appointment SetAnAppointment(Doctor doctor, Patient patient)
        {
            return new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .WithDate(DateTime.UtcNow)
                .SetAppointment();
        }

        private Patient CreateAPatient()
        {
            var patient = new PatientBuilder().CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            return patient;
        }

        private Doctor CreateADoctor()
        {
            var doctor = new DoctorBuilder().CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            return doctor;
        }
        private Appointment UpdateCreatedAppointment()
        {
            var updatedDoctor = new DoctorBuilder().WithFistName("edited").CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(updatedDoctor));
            var UpdatedPatient = new PatientBuilder().WithFirstName("edited").CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(UpdatedPatient));
            var updatedAppointment = new Appointment
            {
                DoctorId = updatedDoctor.Id,
                PatientId = UpdatedPatient.Id,
                Date = DateTime.Today
            };
            return updatedAppointment;
        }

        private Appointment SetAnAppointment()
        {
            var doctor = new DoctorBuilder().CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var patient = new PatientBuilder().CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var appointment = new AppintmentBuilder()
                .WithDoctorId(doctor.Id)
                .WithPatientId(patient.Id)
                .SetAppointment();
            _dataContext.Manipulate(_ => _.Appointments.Add(appointment));
            return appointment;
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
        private List<Patient> CreateSixPatients()
        {
            var patients = new List<Patient> {
                new PatientBuilder().CreatePatient(),
                new PatientBuilder().WithNationalCode("23789").CreatePatient(),
                new PatientBuilder().WithFirstName("delvin").CreatePatient(),
                new PatientBuilder().WithLastName("asadi").CreatePatient(),
                new PatientBuilder().WithFirstName("laaya").CreatePatient(),
                new PatientBuilder().WithNationalCode("1001").CreatePatient(),

            };

            _dataContext.Manipulate(_ => _.Patients.AddRange(patients));
            return patients;
        }


    }
}