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
    public class TourBookingService : ITourBookingService
    {
        private ITourBookingRepository _tourBookingRepository;
        private VoucherService _voucherService;
        public TourBookingService()
        {
            _tourBookingRepository = InjectorRepository.CreateInstance<ITourBookingRepository>();
            _voucherService = new VoucherService();
            BindTourEvent();
            BindVoucher();
        }
        private void BindTourEvent()
        {
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
            {
                int tourEventId = tourBooking.TourEvent.Id;
                TourEvent tourEvent = TourEventRepository.GetInstance().Get(tourEventId);
                if (tourEvent != null)
                {
                    tourBooking.TourEvent = tourEvent;
                }
                else
                {
                    Console.WriteLine("Error in tourReservationTourEvent binding");
                }
            }
        }

        private void BindVoucher()
        {
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
            {
                int voucherId = tourBooking.TourEvent.Id;
                Voucher voucher = VoucherRepository.GetInstance().Get(voucherId);
                if (voucher != null)
                {
                    tourBooking.Voucher = voucher;
                }
                else
                {
                    Console.WriteLine("Error in tourReservationTourEvent binding");
                }
            }
        }

        public List<TourBooking> GetAll()
        {
            return _tourBookingRepository.GetAll();
        }

        public TourBooking Get(int id)
        {
            return _tourBookingRepository.Get(id);
        }
        public void Save()
        {

            _tourBookingRepository.Save();
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


            return _tourBookingRepository.Create(tourBooking);
        }


        public void Delete(TourBooking tourBooking)
        {
            _tourBookingRepository.Delete(tourBooking);
        }

        public TourBooking Update(TourBooking tourBooking)
        {
            return _tourBookingRepository.Update(tourBooking);
        }

        public void Subscribe(IObserver observer)
        {
            _tourBookingRepository.Subscribe(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _tourBookingRepository.Unsubscribe(observer);
        }

      
        public List<TourEvent> TouristTourEvents(int userId)
        {
            List<TourEvent> tourEvents = new List<TourEvent>();
            foreach (TourBooking tourReservation in _tourBookingRepository.GetAll())
            {
                if (tourReservation.TouristId == userId)
                {
                    tourEvents.Add(tourReservation.TourEvent);
                }
            }
            return tourEvents;
        }

        public TourBooking GetTourBookingForTourEventAndUser(int tourEventId, int userId)
        {
            foreach (TourBooking tourReservation in _tourBookingRepository.GetAll())
            {
                if (tourReservation.TouristId == userId && tourReservation.TourEvent.Id == tourEventId)
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
