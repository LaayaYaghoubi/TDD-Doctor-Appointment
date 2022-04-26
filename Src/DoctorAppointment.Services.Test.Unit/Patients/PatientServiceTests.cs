using BookStore.Infrastructure.Test;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Patients;
using DoctorAppointment.Services.Patients;
using DoctorAppointment.Services.Patients.Contracts;
using DoctorAppointment.Tests.Tools.Patients;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointment.Services.Test.Unit.Patients
{
    public class PatientServiceTests
    {
        private readonly PatientService _sut;
        private readonly PatientRepository _repository;
        private readonly ApplicationDbContext _dataContext;
        private readonly UnitOfWork _unitOfWork;

        public PatientServiceTests()
        {
            _dataContext =
                  new EFInMemoryDatabase()
                  .CreateDataContext<ApplicationDbContext>();
            _repository = new EFPatientRepository(_dataContext);
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _sut = new PatientAppService(_repository, _unitOfWork);

        }

        [Fact]
        public void Add_adds_patient_properly()
        {
            var patient = new PatientBuilder().CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));

            _sut.Add(patient);

            var expected = _dataContext.Patients.FirstOrDefault();
            expected.FirstName.Should().Be(patient.FirstName);
            expected.LastName.Should().Be(patient.LastName);
            expected.NationalCode.Should().Be(patient.NationalCode);
            
        }
        [Fact]
        public void Update_updates_patient_properly()
        {
            var patient = new PatientBuilder().CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var updatedPatient = new PatientBuilder().WithFirstName("fili").CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(updatedPatient));

            _sut.Update(patient.Id, updatedPatient);

            var expected = _dataContext.Patients.FirstOrDefault(_ => _.Id == updatedPatient.Id);
            expected.FirstName.Should().Be(updatedPatient.FirstName);
            expected.LastName.Should().Be(updatedPatient.LastName);
            expected.NationalCode.Should().Be(updatedPatient.NationalCode);
            
        }
        [Fact]
        public void Update_throws_PatientWithThisIdDoesNotExistException_if_doctor_doesnot_exist()
        {
            int FakeId = 987;
            var patient = new PatientBuilder().CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(patient));
            var updatedPatient = new PatientBuilder().WithFirstName("joje").CreatePatient();
            _dataContext.Manipulate(_ => _.Patients.Add(updatedPatient));

            Action expected = () => _sut.Update(FakeId, updatedPatient);

            expected.Should().ThrowExactly<PatientWithThisIdDoesNotExistException>();
        }

        //[Fact]
        //public void Delete_deletes_doctor_properly()
        //{
        //    var doctor = new DoctorBuilder().CreateDoctor();
        //    _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

        //    _sut.Delete(doctor.Id);

        //    _dataContext.Doctors.Should().NotContain(doctor);
        //}
        //[Fact]
        //public void Delete_throws_DoctorWithThisIdDoesNotExistException_if_doctor_doesnot_exist()
        //{
        //    int FakeId = 134;
        //    var doctor = new DoctorBuilder().CreateDoctor();
        //    _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

        //    Action expected = () => _sut.Delete(FakeId);

        //    expected.Should().ThrowExactly<DoctorWithThisIdDoesNotExistException>();

        //}
        //[Fact]
        //public void GetAll_returns_all_doctors_properly()
        //{
        //    var doctors = new List<Doctor>()
        //   {
        //       new DoctorBuilder().CreateDoctor(),
        //       new DoctorBuilder().WithFistName("ali")
        //       .WithLastName("moghimi")
        //       .WithNationalCode("23456")
        //       .WithField("gosh").CreateDoctor()

        //};
        //    _dataContext.Manipulate(_ => _.Doctors.AddRange(doctors));

        //    var expected = _sut.GetAll();

        //    expected.Should().HaveCount(2);
        //    expected.Should().Contain(_ => _.FirstName == doctors[0].FirstName);
        //    expected.Should().Contain(_ => _.LastName == doctors[0].LastName);
        //    expected.Should().Contain(_ => _.Field == doctors[0].Field);
        //    expected.Should().Contain(_ => _.NationalCode == doctors[0].NationalCode);
        //    expected.Should().Contain(_ => _.FirstName == doctors[1].FirstName);
        //    expected.Should().Contain(_ => _.LastName == doctors[1].LastName);
        //    expected.Should().Contain(_ => _.NationalCode == doctors[1].NationalCode);
        //    expected.Should().Contain(_ => _.Field == doctors[1].Field);

        //}
    }
}
