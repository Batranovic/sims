using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Models;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class GuestRatingRepository : IRepository<GuestRating>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/ratingGuest.csv";
        private readonly List<IObserver> _observers;

        private readonly Serializer<GuestRating> _serializer;

        private List<GuestRating> _ratingGuests;
        public ReservationRepository ReservationDAO { get; set; }

        private static GuestRatingRepository _instance = null;

        public static GuestRatingRepository GetInstance()
        {
            if(_instance  == null)
            {
                _instance = new GuestRatingRepository();
            }
            return _instance;
        }
        private GuestRatingRepository()
        {
            _serializer = new Serializer<GuestRating>();
            _observers = new List<IObserver>();
            _ratingGuests = new List<GuestRating>();
            _ratingGuests = _serializer.FromCSV(_filePath);
            ReservationDAO = ReservationRepository.GetInstance();
        }
        public GuestRating Create(GuestRating entity)
        {
            entity.Id = NextId();
            _ratingGuests.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }
        public GuestRating Delete(GuestRating entity)
        {
            _ratingGuests.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }
        public GuestRating Get(int id)
        {
            return _ratingGuests.Find(r => r.Id == id);
        }

        public List<GuestRating> GetAll()
        {
            return _ratingGuests;
        }
        public int NextId()
        {
            if (_ratingGuests.Count == 0)
                return 0;
            int nextId = _ratingGuests[_ratingGuests.Count - 1].Id + 1;
            foreach (GuestRating r in _ratingGuests)
            {
                if (nextId == r.Id)
                {
                    nextId++;
                }
            }
            return nextId;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _ratingGuests); 
        }
        public GuestRating Update(GuestRating entity)
        {
            var oldEntity = Get(entity.Id);
            if (oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            NotifyObservers();
            return oldEntity;
        }
        public void BindReservation()
        {
            foreach(GuestRating r in _ratingGuests)
            {
                r.Reservation = ReservationDAO.Get(r.IdReservation);
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
