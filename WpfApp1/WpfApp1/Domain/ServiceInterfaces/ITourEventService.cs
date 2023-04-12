using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Domain.Models;
using WpfApp.Observer;

namespace WpfApp1.Domain.ServiceInterfaces
{
    internal interface ITourEventService : IService<TourEvent>
    {
        void Create(TourEvent entity);
        void Delete(TourEvent entity);
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);

        public int CheckAvailability(TourEvent tourEvent);
        public List<TourBooking> GetAllTourBookingsForTourEvent(TourEvent tourEvent);

        public List<TourEvent> GetAvailableTourEventsForLocation(Location location, int numberOfPeople);

        public List<TourEvent> GetNotFinishedTourEvents(Tour tour);
    }
}
