using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Serializer;
using WpfApp1.Domain.RepositoryInterfaces;

namespace WpfApp1.Repository
{
    public class TourBookingRepository : ITourBookingRepository
    {
        private const string _filePath = "../../../Resources/Data/tourBookings.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<TourBooking> _serializer;

        private List<TourBooking> _tourBookings;

        private static TourBookingRepository instance = null;

        private TourBookingRepository()
        {
            _serializer = new Serializer<TourBooking>();
            _tourBookings = new List<TourBooking>();
            _tourBookings = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        
        public TourBooking Get(int id)
        {
            return _tourBookings.Find(t => t.Id == id);
        }

        public TourBooking Create(TourBooking entity)
        {
            entity.Id = NextId();
            _tourBookings.Add(entity);
            Save();
            return entity;
        }
        public TourBooking Update(TourBooking tourBooking)
        {
            TourBooking current = _tourBookings.Find(tp => tp.Id == tourBooking.Id);
            int index = _tourBookings.IndexOf(current);
            _tourBookings.Remove(current);
            _tourBookings.Insert(index, tourBooking);
            _serializer.ToCSV(_filePath, _tourBookings);
            return tourBooking;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _tourBookings);
        }

        public TourBooking Delete(TourBooking entity)
        {
            _tourBookings.Remove(entity);
            Save();
            return entity;
        }
        public int NextId()
        {
            if (_tourBookings.Count == 0) return 0;
            int newId = _tourBookings[_tourBookings.Count() - 1].Id + 1;
            foreach (TourBooking t in _tourBookings)
            {
                if (newId == t.Id)
                {
                    newId++;
                }
            }
            return newId;
        }

        public List<TourBooking> GetAll()
        {
            return _tourBookings;
        }

        public static TourBookingRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new TourBookingRepository();
            }
            return instance;
        }

        public void BindTourEvent()
        {
            foreach (TourBooking tourBooking in _tourBookings)
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

        public void BindVoucher()
        {
            foreach (TourBooking tourBooking in _tourBookings)
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




        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }

    }
}
