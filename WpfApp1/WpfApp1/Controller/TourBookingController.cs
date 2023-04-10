using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;
using WpfApp1.Repository;
using WpfApp1.Service;
using WpfApp.Observer;

namespace WpfApp1.Controller
{
    public class TourBookingController
    {
        private readonly TourBookingService _tourBookingServices;

        public TourBookingController()
        {
            _tourBookingServices = new TourBookingService();
        }

        public List<TourBooking> GetAll()
        {
            return _tourBookingServices.GetAll();
        }

        public TourBooking Get(int id)
        {
            return _tourBookingServices.Get(id);
        }

        public void Create(TourBooking tourBooking)
        {
            _tourBookingServices.Create(tourBooking);
        }
        public void Update(TourBooking tourBooking)
        {
            _tourBookingServices.Update(tourBooking);
        }

        public void Delete(TourBooking tourBooking)
        {
            _tourBookingServices.Delete(tourBooking);
        }
        public void Subscribe(IObserver observer)
        {
            _tourBookingServices.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourBookingServices.Unsubscribe(observer);
        }

        public List<TourEvent> TouristTourEvents(int userId)
        {
            return _tourBookingServices.TouristTourEvents(userId);
        }

        public TourBooking GetTourBookingForTourEventAndUser(int tourEventId, int userId)
        {
            return _tourBookingServices.GetTourBookingForTourEventAndUser(tourEventId, userId);
        }
    }

}
