using Microsoft.Win32;
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
    public class RatingOwnerDAO : IDAO<RatingOwner>, ISubject
    {
        private const string _filePath = "../../../Resources/Data/ratingOwner.csv";
        private readonly List<IObserver> _observers;

        private readonly Serializer<RatingOwner> _serializer;

        private List<RatingOwner> _ratingOwners;

        public ReservationDAO ReservationDAO { get; set; }

        private static RatingOwnerDAO _instance = null;

        public static RatingOwnerDAO GetInstance()
        {
            if(_instance == null)
            {
                _instance = new RatingOwnerDAO();
            }
            return _instance;
        }

        private RatingOwnerDAO()
        {
            _serializer = new Serializer<RatingOwner>();
            _observers = new List<IObserver>();
            _ratingOwners = new List<RatingOwner>();
            _ratingOwners = _serializer.FromCSV(_filePath);
            ReservationDAO = ReservationDAO.GetInstance();
        }

        public void BindReservation()
        {
            foreach(RatingOwner r in GetAll())
            {
                r.Reservation = ReservationDAO.Get(r.IdReservation);
            }
        }

        public RatingOwner Create(RatingOwner entity)
        {
            entity.Id = NextId();
            _ratingOwners.Add(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public RatingOwner Delete(RatingOwner entity)
        {
            _ratingOwners.Remove(entity);
            Save();
            NotifyObservers();
            return entity;
        }

        public RatingOwner Get(int id)
        {
            return _ratingOwners.Find(r => r.Id == id);
        }

        public List<RatingOwner> GetAll()
        {
            return _ratingOwners;
        }

        public int NextId()
        {
            if (_ratingOwners.Count == 0)
                return 0;
            int nextId = _ratingOwners[_ratingOwners.Count - 1].Id + 1;
            foreach(RatingOwner r in _ratingOwners)
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

        public RatingOwner Update(RatingOwner entity)
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
