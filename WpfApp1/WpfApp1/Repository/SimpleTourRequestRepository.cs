using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.Models;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Serializer;
using WpfApp1.Domain.Models;

namespace WpfApp1.Repository
{
    public class SimpleTourRequestRepository : ISimpleTourRequestRepository
    {
        private const string _filePath = "../../../Resources/Data/simpleTourRequests.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<SimpleTourRequest> _serializer;

        private List<SimpleTourRequest> _simpleTourRequest;
        private static ISimpleTourRequestRepository instance = null;

        private SimpleTourRequestRepository()
        {
            _serializer = new Serializer<SimpleTourRequest>();
            _simpleTourRequest = new List<SimpleTourRequest>();
            _simpleTourRequest = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public static ISimpleTourRequestRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new SimpleTourRequestRepository();
            }
            return instance;
        }

        public SimpleTourRequest Get(int id)
        {
            return _simpleTourRequest.Find(t => t.Id == id);
        }
        public SimpleTourRequest Create(SimpleTourRequest entity)
        {
            entity.Id = NextId();
            _simpleTourRequest.Add(entity);
            Save();
            return entity;
        }
        public SimpleTourRequest Update(SimpleTourRequest tour)
        {

            SimpleTourRequest current = _simpleTourRequest.Find(v => v.Id == tour.Id);
            int index = _simpleTourRequest.IndexOf(current);
            _simpleTourRequest.Remove(current);
            _simpleTourRequest.Insert(index, tour);
            _serializer.ToCSV(_filePath, _simpleTourRequest);
            return tour;
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _simpleTourRequest);
        }
        public SimpleTourRequest Delete(SimpleTourRequest entity)
        {
            _simpleTourRequest.Remove(entity);
            Save();
            return entity;
        }
        public int NextId()
        {
            if (_simpleTourRequest.Count == 0) return 0;
            int newId = _simpleTourRequest[_simpleTourRequest.Count() - 1].Id + 1;
            foreach (SimpleTourRequest a in _simpleTourRequest)
            {
                if (newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }
        public List<SimpleTourRequest> GetAll()
        {
            return _simpleTourRequest;
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
