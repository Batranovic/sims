using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class AccommodationRenovationSuggestionRepository : IAccommodationRenovationSuggestionRepository
    {
        private const string _filePath = "../../../Resources/Data/renovationSuggestions.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<AccommodationRenovationSuggestion> _serializer;
        private static IAccommodationRenovationSuggestionRepository _instance = null;
        private List<AccommodationRenovationSuggestion> _accommodations;

        public static IAccommodationRenovationSuggestionRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AccommodationRenovationSuggestionRepository();
            }
            return _instance;
        }

        private AccommodationRenovationSuggestionRepository()
        {
            _serializer = new Serializer<AccommodationRenovationSuggestion>();
            _accommodations = new List<AccommodationRenovationSuggestion>();
            _accommodations = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public AccommodationRenovationSuggestion Create(AccommodationRenovationSuggestion entity)
        {
            entity.Id = NextId();
            _accommodations.Add(entity);
            Save();
            return entity;
        }
        public AccommodationRenovationSuggestion Delete(AccommodationRenovationSuggestion entity)
        {
            _accommodations.Remove(entity);
            Save();
            return entity;
        }
        public AccommodationRenovationSuggestion Get(int id)
        {
            return _accommodations.Find(a => a.Id == id);
        }
        public List<AccommodationRenovationSuggestion> GetAll()
        {
            return _accommodations;
        }
        public int NextId()
        {
            return _accommodations.Count == 0 ? 0 : _accommodations.Max(a => a.Id)+1;
        }
        public void Save()
        {
            _serializer.ToCSV(_filePath, _accommodations);
        }
        public AccommodationRenovationSuggestion Update(AccommodationRenovationSuggestion entity)
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
