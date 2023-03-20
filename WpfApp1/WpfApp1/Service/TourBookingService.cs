using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Model;

namespace WpfApp1.Service
{
    public class TourBookingService
    {
        private TourBookingDAO _tourBookingDAO;

        public TourBookingService()
        {
            _tourBookingDAO = TourBookingDAO.GetInstance();
        }

        public List<TourBooking> GetAll()
        {
            return _tourBookingDAO.GetAll();
        }

        public TourBooking Get(int id)
        {
            return _tourBookingDAO.Get(id);
        }

        public TourBooking Create(TourBooking tourBooking)
        {
            return _tourBookingDAO.Create(tourBooking);
        }


        public void Delete(TourBooking tourBooking)
        {
            _tourBookingDAO.Delete(tourBooking);
        }

        public TourBooking Update(TourBooking tourBooking)
        {
            return _tourBookingDAO.Update(tourBooking);
        }

    }
}
