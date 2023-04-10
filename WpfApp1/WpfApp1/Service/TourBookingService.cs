using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Repository;
using WpfApp1.Models;
using WpfApp.Observer;

namespace WpfApp1.Service
{
    public class TourBookingService
    {
        private TourBookingRepository _tourBookingDAO;
        private VoucherService _voucherService;
        public TourBookingService()
        {
            _tourBookingDAO = TourBookingRepository.GetInstance();
            _voucherService = new VoucherService();
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
            if(tourBooking.Voucher == null)
            {
                tourBooking.Voucher = new Voucher() { Id = -1};
            }

            if (tourBooking.Voucher.Id != -1)
            {
                tourBooking.Voucher.IsUsed = true;
                _voucherService.Update(tourBooking.Voucher);
            }


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

        public void Subscribe(IObserver observer)
        {
            _tourBookingDAO.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourBookingDAO.Unsubscribe(observer);
        }

      
        public List<TourEvent> TouristTourEvents(int userId)
        {
            List<TourEvent> tourEvents = new List<TourEvent>();
            foreach (TourBooking tourReservation in _tourBookingDAO.GetAll())
            {
                if (tourReservation.Tourist.Id == userId)
                {
                    tourEvents.Add(tourReservation.TourEvent);
                }
            }
            return tourEvents;
        }

        public TourBooking GetTourBookingForTourEventAndUser(int tourEventId, int userId)
        {
            foreach (TourBooking tourReservation in _tourBookingDAO.GetAll())
            {
                if (tourReservation.Tourist.Id == userId && tourReservation.TourEvent.Id == tourEventId)
                {
                    return tourReservation;
                }
            }
            return null;

        }

        public void GetExistingTourBooking(int tourEvent, int user, int numOfPeople)
        {
            TourBooking existingTourBooking = GetTourBookingForTourEventAndUser(tourEvent, user);
            if (existingTourBooking != null)
            {
                existingTourBooking.NumberOfGuests += numOfPeople;
                Update(existingTourBooking);
            }
        }



    }
}
