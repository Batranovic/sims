using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Domain.Models;
using WpfApp.Observer;
using WpfApp1.Domain.ServiceInterfaces;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Service
{
    public class TourEventService : ITourEventService
    {

        private readonly ITourEventRepository _tourEventRepository;
        private readonly ITourBookingRepository _tourBookingRepository;

        public TourEventService()
        {
            _tourEventRepository = InjectorRepository.CreateInstance<ITourEventRepository>();
            _tourBookingRepository = InjectorRepository.CreateInstance<ITourBookingRepository>();
            BindTour();
        }
        private void BindTour()
        {
            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                int tourId = tourEvent.Tour.Id;
                Tour tour = TourRepository.GetInstance().Get(tourId);
                if (tour != null)
                {
                    tourEvent.Tour = tour;
                    tour.TourEvents.Add(tourEvent);
                }
                else
                {
                    Console.WriteLine("Error in binding tour and tourEvent");
                }
            }
        }
        public List<TourEvent> GetAll()
        {
            return _tourEventRepository.GetAll();
        }

        public TourEvent Get(int id)
        {
            return _tourEventRepository.Get(id);
        }

        public void Save()
        {

            _tourEventRepository.Save();
        }

        public void Delete(TourEvent tourEvent)
        {

            _tourEventRepository.Delete(tourEvent);

        }

        public TourEvent Update(TourEvent tourEvent)
        {
            return _tourEventRepository.Update(tourEvent);
        }

        public void Create(TourEvent entity)
        {
             _tourEventRepository.Create(entity);
        }


        public void Subscribe(IObserver observer)
        {
            _tourEventRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourEventRepository.Unsubscribe(observer);
        }



        public int CheckAvailability(TourEvent tourEvent)
        {
            int numOfPeople = 0;
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
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
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
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


            foreach (TourEvent tourEvent in _tourEventRepository.GetAll())
            {
                int freePlaces = tourEvent.Tour.MaxGuests - CheckAvailability(tourEvent);
                if (tourEvent.StartTime > DateTime.Now && tourEvent.Tour.Location.City == location.City && tourEvent.Tour.Location.State == location.State && freePlaces > numberOfPeople)
                {
                    tourEvents.Add(tourEvent);
                }
            }
            return tourEvents;
        }
        public List<TourEvent> GetNotFinishedTourEvents(Tour tour)
        {
            List<TourEvent> tourEventsNotPassed = new List<TourEvent>();

            foreach (TourEvent tourEvent in tour.TourEvents)
            {
                if (tourEvent.StartTime.Date > DateTime.Now.Date)
                {
                    tourEventsNotPassed.Add(tourEvent);
                }
            }
            foreach (TourEvent tourEvent1 in _tourEventRepository.GetAll())
            {
                if (tourEvent1.Tour.Location.City == tour.Location.City)
                {
                    tourEventsNotPassed.Add(tourEvent1);
                }
            }

            return tourEventsNotPassed;
        }


    }
}
