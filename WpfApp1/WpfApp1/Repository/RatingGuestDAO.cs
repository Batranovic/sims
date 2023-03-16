using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class RatingGuestDAO : IDAO<RatingGuest>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/ratingGuest.csv";
        private readonly List<IObserver> _observers;

        private readonly Serializer<RatingGuest> _serializer;

        private List<RatingGuest> _ratingGuests;

        public AccommodationDAO AccommodationDAO { get;  set; }

        public RatingGuestDAO()
        {
            _serializer = new Serializer<RatingGuest>();
            _observers = new List<IObserver>();
            _ratingGuests = new List<RatingGuest>();
            _ratingGuests = _serializer.FromCSV(_filePath);
        }

        public RatingGuest Create(RatingGuest entity)
        {
            entity.Id = NextId();
            _ratingGuests.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }
        
        public RatingGuest Delete(RatingGuest entity)
        {
            _ratingGuests.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public RatingGuest Get(int id)
        {
            return _ratingGuests.Find(r => r.Id == id);
        }

        public List<RatingGuest> GetAll()
        {
            return _ratingGuests;
        }

        public int NextId()
        {
            if (_ratingGuests.Count == 0)
                return 0;
            int nextId = _ratingGuests[_ratingGuests.Count - 1].Id + 1;
            foreach (RatingGuest r in _ratingGuests)
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
        public RatingGuest Update(RatingGuest entity)
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

        public void BindAccommodation()
        {
            foreach(RatingGuest r in _ratingGuests)
            {
                r.Accommodation = AccommodationDAO.Get(r.IdAccommodation);
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
