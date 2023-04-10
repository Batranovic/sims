using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Repository;

namespace WpfApp1.Domain.RepositoryInterfaces
{
    public interface ITourBookingRepository : IRepository<TourBooking>, ISubject
    {
        TourBooking Create(TourBooking entity);
        TourBooking Delete(TourBooking entity);
        int NextId();

        void BindTourEvent();

        void BindVoucher();
    }
}
