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
    public class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string _filePath = "../../../Resources/Data/complexTourRequests.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _complexTourRequest;
        private static IComplexTourRequestRepository instance = null;

        private ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            _complexTourRequest = new List<ComplexTourRequest>();
            _complexTourRequest = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public static IComplexTourRequestRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ComplexTourRequestRepository();
            }
            return instance;
        }
        public ComplexTourRequest Get(int id)
        {
            return _complexTourRequest.Find(t => t.Id == id);
        }
        public ComplexTourRequest Create(ComplexTourRequest entity)
        {
            entity.Id = NextId();
            _complexTourRequest.Add(entity);
            Save();
            return entity;
        }
        public ComplexTourRequest Update(ComplexTourRequest tour)
        {

            ComplexTourRequest current = _complexTourRequest.Find(v => v.Id == tour.Id);
            int index = _complexTourRequest.IndexOf(current);
            _complexTourRequest.Remove(current);
            _complexTourRequest.Insert(index, tour);
            _serializer.ToCSV(_filePath, _complexTourRequest);
            return tour;
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _complexTourRequest);
        }
        public ComplexTourRequest Delete(ComplexTourRequest entity)
        {
            _complexTourRequest.Remove(entity);
            Save();
            return entity;
        }
        public int NextId()
        {
            if (_complexTourRequest.Count == 0) return 0;
            int newId = _complexTourRequest[_complexTourRequest.Count() - 1].Id + 1;
            foreach (ComplexTourRequest a in _complexTourRequest)
            {
                if (newId == a.Id)
                {
                    newId++;
                }
            }
            return newId;
        }
        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequest;
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
