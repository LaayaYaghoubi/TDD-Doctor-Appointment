using BookStore.Infrastructure.Test;
using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Infrastructure.Application;
using DoctorAppointment.Persistence.EF;
using DoctorAppointment.Persistence.EF.Doctors;
using DoctorAppointment.Services.Doctors;
using DoctorAppointment.Services.Doctors.Contracts;
using DoctorAppointment.Tests.Tools.Doctors;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DoctorAppointment.Services.Test.Unit.Doctors
{
    public class DoctorServiceTests
    {
        private readonly DoctorService _sut;
        private readonly DoctorRepository _repository;
        private readonly ApplicationDbContext _dataContext;
        private readonly UnitOfWork _unitOfWork;

        public DoctorServiceTests()
        {
            _dataContext =
                  new EFInMemoryDatabase()
                  .CreateDataContext<ApplicationDbContext>();
            _repository = new EFDoctorRepository(_dataContext);
            _unitOfWork = new EFUnitOfWork(_dataContext);
            _sut = new DoctorAppService(_repository, _unitOfWork);

        }

        [Fact]
        public void Add_adds_doctor_properly()
        {
            var doctor = new DoctorBuilder().CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));

            _sut.Add(doctor);

            var expected = _dataContext.Doctors.FirstOrDefault();
            expected.FirstName.Should().Be(doctor.FirstName);
            expected.LastName.Should().Be(doctor.LastName);
            expected.NationalCode.Should().Be(doctor.NationalCode);
            expected.Field.Should().Be(doctor.Field);
        }
        [Fact]
        public void Update_updates_doctor_properly()
        {
            var doctor = new DoctorBuilder().CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(doctor));
            var updatedDoctor = new DoctorBuilder().WithFistName("gholi").CreateDoctor();
            _dataContext.Manipulate(_ => _.Doctors.Add(updatedDoctor));

            _sut.Update(doctor.Id, updatedDoctor);

            var expected = _dataContext.Doctors.FirstOrDefault(_ => _.Id == updatedDoctor.Id);
            expected.FirstName.Should().Be(updatedDoctor.FirstName);
            expected.LastName.Should().Be(updatedDoctor.LastName);
            expected.NationalCode.Should().Be(updatedDoctor.NationalCode);
            expected.Field.Should().Be(updatedDoctor.Field);
            


        }

    }
}
