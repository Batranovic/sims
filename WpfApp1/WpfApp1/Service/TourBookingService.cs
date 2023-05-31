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
using System.Windows;

namespace WpfApp1.Service 
{
    public class TourBookingService : ITourBookingService
    {
        private ITourBookingRepository _tourBookingRepository;
        private IVoucherRepository _voucherRepository;
        public TourBookingService()
        {
            _tourBookingRepository = InjectorRepository.CreateInstance<ITourBookingRepository>();
            _voucherRepository = InjectorRepository.CreateInstance<IVoucherRepository>();
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
                _voucherRepository.Update(tourBooking.Voucher);
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
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
            {
                if (tourBooking.Tourist.Id == userId)
                {
                    tourEvents.Add(tourBooking.TourEvent);
                }
            }
            return tourEvents;
        }

        public TourBooking GetTourBookingForTourEventAndUser(int tourEventId, int userId)
        {
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
            {
                if (tourBooking.Tourist.Id == userId && tourBooking.TourEvent.Id == tourEventId)
                {
                    return tourBooking;
                }
            }
            return null;

        }

        public List<TourBooking> GetTourBookingsForTourist(int userId)
        {
            List<TourBooking> bookings = new List<TourBooking>();
            foreach (TourBooking tourBooking in _tourBookingRepository.GetAll())
            {
                if (tourBooking.Tourist.Id == userId)
                {
                    bookings.Add(tourBooking);
                }
            }
            return bookings;
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

        public Voucher WonVoucher(int userId)
        {
            var tourBookings = _tourBookingRepository.GetAll();
            var filteredTourBookings = tourBookings.Where(t => t.Tourist.Id == userId);

            var indexedTourBookings = filteredTourBookings.Select((booking, index) => new { Booking = booking, Index = index });

            var fifthBookings = indexedTourBookings.Where(x => (x.Index + 1) % 5 == 0).Select(x => x.Booking);

            if (fifthBookings.Any())
            {
                var lastFifthBooking = fifthBookings.Last();

                DateTime sixMonthsLater = DateTime.Now.AddMonths(6);

                Voucher newVoucher = new Voucher
                {
                    Tourist = new Tourist { Id = userId },
                    ExpirationDate = sixMonthsLater,
                    Name = "voucher"
                };

                _voucherRepository.Create(newVoucher);
                _voucherRepository.Update(newVoucher);

                return newVoucher;
            }

            return null;
        }





    }
}
