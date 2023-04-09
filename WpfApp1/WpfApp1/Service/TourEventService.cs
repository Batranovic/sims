using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Model;
using WpfApp.Observer;

namespace WpfApp1.Service
{
    public class TourEventService
    {

        private TourEventRepository _tourEventDAO;
        private TourBookingRepository _tourBookingDAO;

        public TourEventService()
        {
            _tourEventDAO = TourEventRepository.GetInstance();
            _tourBookingDAO = TourBookingRepository.GetInstance();
        }

        public List<TourEvent> GetAll()
        {
            return _tourEventDAO.GetAll();
        }

        public TourEvent Get(int id)
        {
            return _tourEventDAO.Get(id);
        }



        public void Delete(TourEvent tourEvent)
        {

            _tourEventDAO.Delete(tourEvent);

        }

        public TourEvent Update(TourEvent tourEvent)
        {
            return _tourEventDAO.Update(tourEvent);
        }


        public void Subscribe(IObserver observer)
        {
            _tourEventDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourEventDAO.Unsubscribe(observer);
        }



        public int CheckAvailability(TourEvent tourEvent)
        {
            int numOfPeople = 0;
            foreach (TourBooking tourBooking in _tourBookingDAO.GetAll())
            {
                if (tourBooking.TourEvent.Id == tourEvent.Id)
                {
                    numOfPeople += tourBooking.NumberOfGuests;
                }
            }
            return numOfPeople;
        }

        public List<TourBooking> GetAllTourBookingsForTourEvent(TourEvent tourEvent)
        {
            List<TourBooking> tourBookingList = new List<TourBooking>();
            foreach (TourBooking tourBooking in _tourBookingDAO.GetAll())
            {
                if (tourBooking.TourEvent.Id == tourEvent.Id)
                {
                    tourBookingList.Add(tourBooking);
                }
            }
            return tourBookingList;
        }

        public List<TourEvent> GetAvailableTourEventsForLocation(Location location, int numberOfPeople)
        {
            List<TourEvent> tourEvents = new List<TourEvent>();


            foreach (TourEvent tourEvent in _tourEventDAO.GetAll())
            {
                int freePlaces = tourEvent.Tour.MaxGuests - CheckAvailability(tourEvent);
                if (tourEvent.StartTime > DateTime.Now && tourEvent.Tour.Location.City == location.City && tourEvent.Tour.Location.State == location.State && freePlaces > numberOfPeople)
                {
                    tourEvents.Add(tourEvent);
                }
            }
            return tourEvents;
        }
    }
}
