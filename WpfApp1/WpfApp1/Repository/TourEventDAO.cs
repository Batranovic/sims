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
    public class TourEventDAO : IDAO<TourEvent>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/tourEvents.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<TourEvent> _serializer;

        private List<TourEvent> _tourEvents;

        private static TourEventDAO instance = null;

        private TourEventDAO()
        {
            _serializer = new Serializer<TourEvent>();
            _tourEvents = new List<TourEvent>();
            _tourEvents = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }


        public TourEvent Get(int id)
        {
            return _tourEvents.Find(t => t.Id == id);
        }
        public TourEvent Create(TourEvent entity)
        {
            entity.Id = NextId();
            _tourEvents.Add(entity);
            Save();
            return entity;
        }
        public TourEvent Update(TourEvent entity)
        {
            var oldEntity = Get(entity.Id);
            if (oldEntity == null)
            {
                return null;
            }
            oldEntity = entity;
            Save();
            return oldEntity;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _tourEvents);
        }

        public TourEvent Delete(TourEvent entity)
        {
            _tourEvents.Remove(entity);
            Save();
            return entity;
        }
        public int NextId()
        {
            if (_tourEvents.Count == 0) return 0;
            int newId = _tourEvents[_tourEvents.Count() - 1].Id + 1;
            foreach (TourEvent t in _tourEvents)
            {
                if (newId == t.Id)
                {
                    newId++;
                }
            }
            return newId;
        }

        public List<TourEvent> GetAll()
        {
            return _tourEvents;
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

        public static TourEventDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new TourEventDAO();
            }
            return instance;
        }

        public void BindTourEventTour()
        {
            foreach (TourEvent tourEvent in _tourEvents)
            {
                int tourId = tourEvent.Tour.Id;
                Tour tour = TourDAO.GetInstance().Get(tourId);
                if (tour != null)
                {
                    tourEvent.Tour = tour;
                    tour.TourEvents.Add(tourEvent);
                }
                else
                {
                    Console.WriteLine("Error in accommodationLocation binding");
                }
            }
        }
    }
}
