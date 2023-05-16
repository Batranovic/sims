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
    class RenovationRepository : IRenovationRepository
    {
        private const string _filePath = "../../../Resources/Data/renovations.csv";
        private readonly List<IObserver> _observers;
        private readonly Serializer<Renovation> _serializer;
        private static IRenovationRepository _instance = null;
        private List<Renovation> _renovations;

        public static IRenovationRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RenovationRepository();
            }
            return _instance;
        }
        private RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = new List<Renovation>();
            _renovations = _serializer.FromCSV(_filePath);
            _observers = new List<IObserver>();
        }

        public Renovation Create(Renovation entity)
        {
            entity.Id = NextId();
            _renovations.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public Renovation Delete(Renovation entity)
        {
            entity.IsCanceled = true;
            Save();
            NotifyObservers();
            return entity;
        }
        
        public Renovation Get(int id)
        {
            return _renovations.Find(a => a.Id == id);
        }
        
        public List<Renovation> GetAll()
        {
            return _renovations;
        }
        
        public int NextId()
        {
            if (_renovations.Count == 0) return 0;
            int newId = _renovations[_renovations.Count() - 1].Id + 1;
            foreach (Renovation r in _renovations)
            {
                if (newId == r.Id)
                {
                    newId++;
                }
            }
            return newId;
        }
        
        public void Save()
        {
            _serializer.ToCSV(_filePath, _renovations);
        }
        
        public Renovation Update(Renovation entity)
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
