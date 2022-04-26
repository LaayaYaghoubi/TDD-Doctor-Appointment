﻿

using DoctorAppointment.Entities.Doctors;
using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Services.Doctors.Contracts
{
    public interface DoctorService : Service 
    {
        void Add(Doctor doctor);
        void Update(int id, Doctor updatedDoctor);
    }
}
