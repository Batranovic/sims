using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;
using WpfApp1.Repository;

namespace WpfApp1.Controller
{
    public class TourBookingController
    {
        private readonly TourBookingDAO _tourBookings;

        public TourBookingController(TourBookingDAO tourBookingDAO)
        {
            _tourBookings = tourBookingDAO;
        }

        public List<TourBooking> GetAll()
        {
            return _tourBookings.GetAll();
        }

        public TourBooking Get(int id)
        {
            return _tourBookings.Get(id);
        }

        public void Create(TourBooking tourBooking)
        {
            _tourBookings.Create(tourBooking);
        }
        public void Update(TourBooking tourBooking)
        {
            _tourBookings.Update(tourBooking);
        }

        public void Delete(TourBooking tourBooking)
        {
            _tourBookings.Delete(tourBooking);
        }

    }

}
