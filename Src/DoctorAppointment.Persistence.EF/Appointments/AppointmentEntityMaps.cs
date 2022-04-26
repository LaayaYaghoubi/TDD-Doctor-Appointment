﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DoctorAppointment.Entities.Appointments;

namespace DoctorAppointment.Persistence.EF.Appointments
{
    public class AppointmentEntityMaps : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> _)
        {
            _.ToTable("Appointments");
            _.HasKey(_ => _.Id);
            _.Property(_ => _.Id)
                .ValueGeneratedOnAdd();

            _.Property(_ => _.Date)
                .IsRequired();

            _.HasOne(_ => _.Doctor)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.DoctorId);

            _.HasOne(_ => _.Patient)
                .WithMany(_ => _.Appointments)
                .HasForeignKey(_ => _.PatientId);
        }
    }
}
