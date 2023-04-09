using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Observer;
using WpfApp1.Domain.RepositoryInterfaces;
using WpfApp1.Model;
using WpfApp1.Serializer;

namespace WpfApp1.Repository
{
    public class OwnerRatingRepository : IOwnerRatingRepository
    {
        private const string _filePath = "../../../Resources/Data/ratingOwner.csv";
        private readonly List<IObserver> _observers;

        private readonly Serializer<OwnerRating> _serializer;

        private List<OwnerRating> _ratingOwners;

        private static IOwnerRatingRepository _instance = null;

        public static IOwnerRatingRepository GetInstance()
        {
            if(_instance == null)
            {
                _instance = new OwnerRatingRepository();
            }
            return _instance;
        }

        private OwnerRatingRepository()
        {
            _serializer = new Serializer<OwnerRating>();
            _observers = new List<IObserver>();
            _ratingOwners = new List<OwnerRating>();
            _ratingOwners = _serializer.FromCSV(_filePath);
        }

        public OwnerRating Create(OwnerRating entity)
        {
            entity.Id = NextId();
            _ratingOwners.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public OwnerRating Delete(OwnerRating entity)
        {
            _ratingOwners.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public OwnerRating Get(int id)
        {
            return _ratingOwners.Find(r => r.Id == id);
        }

        public List<OwnerRating> GetAll()
        {
            return _ratingOwners;
        }

        public int NextId()
        {
            if (_ratingOwners.Count == 0)
                return 0;
            int nextId = _ratingOwners[_ratingOwners.Count - 1].Id + 1;
            foreach(OwnerRating r in _ratingOwners)
            {
                if(nextId == r.Id)
                {
                    nextId++;
                }
            }
            return nextId;
        }

        public void Save()
        {
            _serializer.ToCSV(_filePath, _ratingOwners);
        }

        public OwnerRating Update(OwnerRating entity)
        {
            var oldEntity = Get(entity.Id);
            if(oldEntity == null)
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
