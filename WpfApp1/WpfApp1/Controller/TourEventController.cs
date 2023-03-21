using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Service;
using WpfApp1.Model;

namespace WpfApp1.Controller
{
    public class TourEventController
    {

        private readonly TourEventService _tourEventService;

        public TourEventController()
        {
            _tourEventService = new TourEventService();
        }

        public List<TourEvent> GetAll()
        {
            return _tourEventService.GetAll();
        }

        public TourEvent Get(int id)
        {
            return _tourEventService.Get(id);
        }



        public void Delete(TourEvent tourEvent)
        {

            _tourEventService.Delete(tourEvent);

        }

        public TourEvent Update(TourEvent tourEvent)
        {
            return _tourEventService.Update(tourEvent);
        }



        public int CheckAvailability(TourEvent tourEvent)
        {
            return _tourEventService.CheckAvailability(tourEvent);
        }

        public List<TourBooking> GetAllTourReservationForTourEvent(TourEvent tourEvent)
        {
            return _tourEventService.GetAllTourBookingsForTourEvent(tourEvent);
        }

        public List<TourEvent> GetAvailableTourEventsForLocation(Location location, int numberOfPeople)
        {
            return _tourEventService.GetAvailableTourEventsForLocation(location, numberOfPeople);
        }




    }
   


}
