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
    public class TourDAO : IDAO<Tour>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/tours.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public LocationDAO LocationDAO { get; set; }

        public TourDAO()
        {
            _serializer = new Serializer<Tour>();
            _tours = new List<Tour>();
            _tours = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public void BindLocation()
        {
            foreach (Tour a in _tours)
            {
                a.Location = LocationDAO.Get(a.IdLocation);
            }
        }
        public Tour Get(int id)
        {
            return _tours.Find(a => a.Id == id);
        }
        public Tour Create(Tour entity)
        {
            entity.Id = NextId();
            _tours.Add(entity);
            Save();
            return entity;
        }
        public Tour Update(Tour entity)
        {
            var oldEntity = Get(entity.Id);
            if(oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _tours);
        }

        public Tour Delete(Tour entity)
        {
            _tours.Remove(entity);
            Save();
            return entity;
        }
        public int NextId()
        {
            if (_tours.Count == 0) return 0;
            int newId = _tours[_tours.Count() - 1].Id + 1;
            foreach(Tour a in _tours)
            {
                if (newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }

        public List<Tour> GetAll()
        {
            return _tours;
        }

        public List<Tour> Search(Location location, TimeSpan duration, string language, int maxGuests)
        {
            List<Tour> searchedTours = new List<Tour>();

            foreach (Tour tour in _tours)
            {
                if (string.Equals(tour.Location.City, location.City, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(tour.Location.State, location.State, StringComparison.OrdinalIgnoreCase)
                    && tour.Duration == duration
                    && string.Equals(tour.Language, language, StringComparison.OrdinalIgnoreCase)
                    && tour.MaxGuests >= maxGuests)
                {
                    searchedTours.Add(tour);
                }
            }
            return searchedTours;
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
