using DoctorAppointment.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Persistence.EF
{
    public class EFUnitOfWork : UnitOfWork
    {
        private readonly ApplicationDbContext _dataContext;

        public EFUnitOfWork(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
